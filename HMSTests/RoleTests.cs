using HmsLibrary.Data.Context;
using HmsLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;
using Moq;
using HmsAPI.Data;

namespace HMSTests;

public class RoleTests
{
    private HmsDbContext _dbContext;
    private IRoleService _roleService;
    private RoleManager<IdentityRole<Guid>> _roleManagerMock;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);
        _dbContext.Database.EnsureDeleted();

        //var userManagerMock = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);

        // Create a mock for RoleStore
        var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();

        // Create an instance of RoleManager
        var roleManager = new RoleManager<IdentityRole<Guid>>(roleStoreMock.Object, null, null, null, null);

        // Setup service
        _roleService = new RoleService(_dbContext, roleManager);
    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    [Test]
    [TestCase("Admin")]
    [TestCase("Doctor")]
    [TestCase("Nurse")]
    [TestCase("Patient")]
    public async Task CreateRoleAsync(string roleName)
    {
        var role = new EmployeeRole
        {
            RoleName = roleName
        };

        var result = await _roleService.CreateRole(role);

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.RoleName, Is.EqualTo(role.RoleName));
        });
    }

    [Test]
    public void CreateAlreadyExistingRole()
    {
        var role = new EmployeeRole
        {
            RoleName = "Admin"
        };

        _dbContext.EmployeeRoles.Add(role);
        _dbContext.SaveChanges();

        Assert.ThrowsAsync<ArgumentException>(async () => await _roleService.CreateRole(role));
    }

    [Test]
    public async Task GetRolesAsync()
    {
        var roles = CreateRoles();

        await _dbContext.EmployeeRoles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        var result = await _roleService.GetRoles();

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.Count, Is.EqualTo(4));
            Assert.That(result[0].RoleName, Is.EqualTo(roles[0].RoleName));
            Assert.That(result[1].RoleName, Is.EqualTo(roles[1].RoleName));
            Assert.That(result[2].RoleName, Is.EqualTo(roles[2].RoleName));
            Assert.That(result[3].RoleName, Is.EqualTo(roles[3].RoleName));
        });
    }

    [Test]
    public async Task GetRoleAsync()
    {
        var roles = CreateRoles();

        await _dbContext.EmployeeRoles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        // Get a list of all the roles
        var createdRoles = _dbContext.EmployeeRoles.ToList();

        var result = await _roleService.GetRole(createdRoles[0].Id);

        Assert.That(result, Is.Not.Null);

        Assert.That(result.RoleName, Is.EqualTo(createdRoles[0].RoleName));
    }

    [Test]
    public async Task GetRoleAsync_InvalidId()
    {
        var roles = CreateRoles();
        await _dbContext.EmployeeRoles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        var result = await _roleService.GetRole(new Guid());

        Assert.That(result, Is.Null);
    }

    [Test]
    public async Task DeleteRoleAsync()
    {
        var roles = CreateRoles();

        await _dbContext.EmployeeRoles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        var createdRoles = _dbContext.EmployeeRoles.ToList();

        var result = await _roleService.DeleteRole(createdRoles[0].Id);

        Assert.That(result, Is.Not.Null);
        
        Assert.That(result.RoleName, Is.EqualTo(createdRoles[0].RoleName));
    }

    [Test]
    [TestCase("00000000-0000-0000-0000-000000000001")] // Sample invalid ID
    [TestCase("00000000-0000-0000-0000-000000000002")]
    public async Task DeleteRoleAsync_InvalidId(Guid id)
    {
        var roles = CreateRoles();

        await _dbContext.EmployeeRoles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        Assert.ThrowsAsync<ArgumentException>(async () => await _roleService.DeleteRole(id));
    }

    // Create a list of roles to use in the test
    private List<EmployeeRole> CreateRoles()
    {
        return
        [
            new EmployeeRole
            {
                RoleName = "Admin"
            },

            new EmployeeRole
            {
                RoleName = "Doctor"
            },

            new EmployeeRole
            {
                RoleName = "Nurse"
            },

            new EmployeeRole
            {
                RoleName = "Patient"
            }
        ];
    }
}