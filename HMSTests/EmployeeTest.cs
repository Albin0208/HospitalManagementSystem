using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.EntityFrameworkCore;

namespace HMSTests;

public class EmployeeTest
{
    private HmsDbContext _dbContext;
    private EmployeeService _employeeService;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);
        _dbContext.Database.EnsureDeleted(); // Delete database before each test

        _employeeService = new EmployeeService(_dbContext);

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
            RoleId = 1,
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
        Assert.ThrowsAsync<ArgumentException>(async () => await _employeeService.DeleteEmployee(1));
    }
}