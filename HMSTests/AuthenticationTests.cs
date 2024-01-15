using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace HMSTests;

public class AuthenticationTests
{
    private HmsDbContext _dbContext;
    private IAuthenticationService _authenticationService;

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
        _authenticationService = new AuthenticationService(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    [Test]
    public void UserCreation()
    {
        var employee = CreateEmployee("John", "John", "Doe",
            "john@test.com", "1994-12-01", "123123123", "Street 2",
            "Fake city", "12312", "Fake country");

        var result = _authenticationService.SignUp(employee, "test");

        Assert.That(result, Is.True);

        var user = _dbContext.Employees.FirstOrDefault(u => u.Username == employee.Username);

        Assert.That(user, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(user.Username, Is.EqualTo(employee.Username));
            Assert.That(user.Password, Is.Not.EqualTo("test")); // Check that the password is encrypted
        });
    }

    [Test]
    public void UserSignIn()
    {
        var employee = CreateEmployee("John", "John", "Doe",
            "john@test.com", "1994-12-01", "123123123", "Street 2",
            "Fake city", "12312", "Fake country");

        // Create new user
        _authenticationService.SignUp(employee, "test");

        var result = _authenticationService.SignIn("John", "test");

        Assert.That(result, Is.True);
    }

    [Test]
    public void SignUpAlreadyCreatedUser()
    {
        var employee = CreateEmployee("John", "John", "Doe",
            "john@test.com", "1994-12-01", "123123123", "Street 2",
            "Fake city", "12312", "Fake country");

        _authenticationService.SignUp(employee, "test");
        var result = _authenticationService.SignUp(employee, "test");

        Assert.That(result, Is.False);
    }

    /// <summary>
    /// Create a new employee with the given parameters
    /// </summary>
    /// <returns>A new employee</returns>
    private Employee CreateEmployee(string? username, string? firstName, string? lastName, string? email, string? dateOfBirth,
        string? phoneNumber, string? address, string? city, string? zipCode, string? country)
    {
        var parsedDateOfBirth = dateOfBirth == null ? default : DateTime.Parse(dateOfBirth);

        return new Employee
        {
            Username = username,
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            DateOfBirth = parsedDateOfBirth,
            PhoneNumber = phoneNumber,
            Address = address,
            City = city,
            ZipCode = zipCode,
            Country = country,
        };
    }
}

