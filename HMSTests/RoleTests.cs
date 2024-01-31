using HmsLibrary.Data.Context;
using HmsLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Model;

namespace HMSTests;

public class RoleTests
{
    private HmsDbContext _dbContext;
    private IRoleService _roleService;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);
        _dbContext.Database.EnsureDeleted();

        // Setup service
        _roleService = new RoleService(_dbContext);
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

        _dbContext.Roles.Add(role);
        _dbContext.SaveChanges();

        Assert.ThrowsAsync<ArgumentException>(async () => await _roleService.CreateRole(role));
    }

    [Test]
    public async Task GetRolesAsync()
    {
        var roles = CreateRoles();

        await _dbContext.Roles.AddRangeAsync(roles);
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
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task GetRoleAsync(int id)
    {
        var roles = CreateRoles();

        await _dbContext.Roles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        var result = await _roleService.GetRole(id);

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.RoleName, Is.EqualTo(roles[id - 1].RoleName));
        });
    }

    [Test]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    public async Task GetRoleAsync_InvalidId(int id)
    {
        var roles = CreateRoles();
        await _dbContext.Roles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        var result = await _roleService.GetRole(id);

        Assert.That(result, Is.Null);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task DeleteRoleAsync(int id)
    {
        var roles = CreateRoles();

        await _dbContext.Roles.AddRangeAsync(roles);
        await _dbContext.SaveChangesAsync();

        var result = await _roleService.DeleteRole(id);

        Assert.That(result, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(result.RoleName, Is.EqualTo(roles[id - 1].RoleName));
        });
    }

    [Test]
    [TestCase(5)]
    [TestCase(6)]
    [TestCase(7)]
    [TestCase(8)]
    public async Task DeleteRoleAsync_InvalidId(int id)
    {
        var roles = CreateRoles();

        await _dbContext.Roles.AddRangeAsync(roles);
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