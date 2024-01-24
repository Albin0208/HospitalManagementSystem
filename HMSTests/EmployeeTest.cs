using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HMSTests;

public class EmployeeTest
{
    private HmsDbContext _dbContext;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);

        // Setup service

    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    [Test]
    [TestCase("John", "Doe", "john.doe", "test", "1994-12-01", "123123123", "Street 2", "Fake city", "12312", "Fake country", null)]
    [TestCase("Jane", "Smith", "jane.smith", "test", "1994-12-01", "123123123", "Street 2", "Fake city", "12312", "Fake country", "doctor")]
    [TestCase("Jane", "Smith", "jane.smith", "test", null, null, null, null, null, null, "nurse")]
    public async Task CreateEmployeeAsync(string? firstname, string? lastname, string? username, string? password, 
        string? dateOfBirth, string? phoneNumber, string? address, string? city, string? zipCode, string? country, string? role)
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

        if (role != null)
        {
            employee.Role = new EmployeeRole { RoleName = role }; // Create a new employeeRole;
        }

        var createdEmployee = await _dbContext.Employees.AddAsync(employee); // Change to use the employee service
        await _dbContext.SaveChangesAsync();

        Assert.That(createdEmployee, Is.Not.Null);
        Assert.That(createdEmployee.Entity.Role, Is.EqualTo(employee.Role));
    }   
}