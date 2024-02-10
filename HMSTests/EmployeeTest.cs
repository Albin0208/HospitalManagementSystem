using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.EntityFrameworkCore;

namespace HMSTests;

public class EmployeeTest
{
    private HmsDbContext _dbContext;
    private IEmployeeService _employeeService;
    private IRoleService _roleService;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);
        _dbContext.Database.EnsureDeleted(); // Delete database before each test

        _roleService = new RoleService(_dbContext);

        _employeeService = new EmployeeService(_dbContext, _roleService);

        // Setup service

    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    [Test]
    [TestCase("John", "Doe", "john.doe", "test", "1994-12-01", "123123123", "Street 2", "Fake city", "12312", "Fake country", "Admin")]
    [TestCase("Jane", "Smith", "jane.smith", "test", "1994-12-01", "123123123", "Street 2", "Fake city", "12312", "Fake country", "doctor")]
    [TestCase("Jane", "Smith", "jane.smith", "test", null, null, null, null, null, null, "nurse")]
    public async Task CreateEmployeeAsync(string? firstname, string? lastname, string? username, string? password,
        string? dateOfBirth, string? phoneNumber, string? address, string? city, string? zipCode, string? country, string? roleName)
    {
        var parsedDateOfBirth = dateOfBirth == null ? default : DateTime.Parse(dateOfBirth);

        var employee = new Employee
        {
            FirstName = firstname,
            LastName = lastname,
            Username = username,
            Password = password,
            DateOfBirth = parsedDateOfBirth,
            PhoneNumber = phoneNumber,
            Address = address,
            City = city,
            ZipCode = zipCode,
            Country = country,
        };

        if (roleName != null)
        {
            var role = new EmployeeRole { RoleName = roleName };

            var id = await _dbContext.Roles.AddAsync(role);
            await _dbContext.SaveChangesAsync();

            employee.RoleId = id.Entity.Id; // Create a new employeeRole;
        }

        var createdEmployee = await _employeeService.CreateEmployee(employee); // Change to use the employee service
        await _dbContext.SaveChangesAsync();

        Assert.That(createdEmployee, Is.Not.Null);
        Assert.That(createdEmployee.Role, Is.EqualTo(employee.Role));
    }

    [Test]
    public void CreateEmployeeWithNoneExistingRole()
    {
        var employee = new Employee
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "john.doe",
            Password = "test",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
            RoleId = new Guid(),
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await _employeeService.CreateEmployee(employee));
    }

    [Test]
    public void DeleteEmployee()
    {
        var employee = new Employee
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "john.doe",
            Password = "test",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        _dbContext.Employees.Add(employee);
        _dbContext.SaveChanges();

        var deletedEmployee = _employeeService.DeleteEmployee(employee.Id);

        Assert.That(deletedEmployee, Is.Not.Null);
    }

    [Test]
    public void DeleteNoneExistingEmployee()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => await _employeeService.DeleteEmployee(new Guid()));
    }

    [Test]
    public async Task UpdateEmployee()
    {
        var employee = new Employee
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "john.doe",
            Password = "test",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();

        var updatedEmployee = new Employee
        {
            Id = employee.Id,
            FirstName = "Jane",
            LastName = "Smith",
            Username = "jane.smith",
            Password = "test",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        var result = await _employeeService.UpdateEmployee(updatedEmployee);

        Assert.That(result, Is.Not.Null);

        Assert.That(result, Is.EqualTo(updatedEmployee));
    }

    [Test]
    public async Task UpdateEmployeeWithFewFields()
    {
        var employee = new Employee
        {
            FirstName = "John",
            LastName = "Doe",
            Username = "john.doe",
            Password = "test",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();

        var updatedEmployee = new Employee
        {
            Id = employee.Id,
            PhoneNumber = "0001012302",
            Address = "Road 4",
            City = "Another city",
            ZipCode = "55555",
        };

        var result = await _employeeService.UpdateEmployee(updatedEmployee);

        Assert.That(result, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Id, Is.EqualTo(updatedEmployee.Id));
            Assert.That(result.FirstName, Is.EqualTo(employee.FirstName));
            Assert.That(result.LastName, Is.EqualTo(employee.LastName));
            Assert.That(result.PhoneNumber, Is.EqualTo(updatedEmployee.PhoneNumber));
            Assert.That(result.Address, Is.EqualTo(updatedEmployee.Address));
            Assert.That(result.City, Is.EqualTo(updatedEmployee.City));
            Assert.That(result.ZipCode, Is.EqualTo(updatedEmployee.ZipCode));
        });
    }

    [Test]
    public void UpdateNoneExistingEmployee()
    {
        var employee = new Employee
        {
            Id = new Guid(),
            FirstName = "John",
            LastName = "Doe",
            Username = "john.doe",
            Password = "test",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await _employeeService.UpdateEmployee(employee));
    }
}