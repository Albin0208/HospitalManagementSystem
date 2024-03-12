using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsAPI.Data;
using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HMSTests;

public class AuthenticationTests
{
    private HmsDbContext _dbContext;
    private IAuthenticationService _authenticationService;
    private Mock<UserManager<ApplicationUser>> _userManagerMock;
    private Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private Mock<IPatientService> _patientService;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);
        _dbContext.Database.EnsureDeleted();

        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            Mock.Of<IUserStore<ApplicationUser>>(),
            null,
            null,
            null,
            null,
            null,
            null,
            null,
            null);

        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
                       Mock.Of<IHttpContextAccessor>(),
                                  Mock.Of<IUserClaimsPrincipalFactory<ApplicationUser>>(),
                                             null,
                                                        null,
                                                                   null,
                                                                              null);

        _patientService = new Mock<IPatientService>();

        // Create a mock for RoleStore
        var roleStoreMock = new Mock<IRoleStore<IdentityRole<Guid>>>();

        // Create an instance of RoleManager
        var roleManager = new RoleManager<IdentityRole<Guid>>(roleStoreMock.Object, null, null, null, null);

        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();

        var userManager = new UserManager<ApplicationUser>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        // Setup service
        _authenticationService = new AuthenticationService(_dbContext, _userManagerMock.Object, _signInManagerMock.Object, _patientService.Object, new EmployeeService(_dbContext, new RoleService(_dbContext, roleManager), userManager));
    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    //[Test]
    //public async Task RegisterPatient_WithValidRequest_ReturnsSuccess()
    //{
    //    // Arrange
    //    var request = new PatientRegisterRequest
    //    {
    //        Email = "test@example.com",
    //        Password = "TestPassword",
    //        FirstName = "John",
    //        LastName = "Doe"
    //        // Add more properties as needed
    //    };

    //    var user = new ApplicationUser { Id = "userId", UserName = request.Email, Email = request.Email };

    //    _userManagerMock.Setup(m => m.CreateAsync(It.IsAny<ApplicationUser>(), request.Password))
    //        .ReturnsAsync(IdentityResult.Success);
    //    _patientService.Setup(m => m.CreatePatient(It.IsAny<Patient>())).ReturnsAsync(true);

    //    // Act
    //    var result = await _authenticationService.RegisterPatient(request);

    //    // Assert
    //    Assert.That(result.Succeeded, Is.True);
    //}

    //[Test]
    //public void UserCreation()
    //{
    //    var employee = CreateEmployee("John", "John", "Doe",
    //        "john@test.com", "1994-12-01", "123123123", "Street 2",
    //        "Fake city", "12312", "Fake country");

    //    var result = _authenticationService.SignUp(employee, "test");

    //    Assert.That(result, Is.True);

    //    var user = _dbContext.Employees.FirstOrDefault(u => u.Username == employee.Username);

    //    Assert.That(user, Is.Not.Null);

    //    Assert.Multiple(() =>
    //    {
    //        Assert.That(user.Username, Is.EqualTo(employee.Username));
    //        Assert.That(user.Password, Is.Not.EqualTo("test")); // Check that the password is encrypted
    //    });
    //}

    //[Test]
    //public void UserSignIn()
    //{
    //    var employee = CreateEmployee("John", "John", "Doe",
    //        "john@test.com", "1994-12-01", "123123123", "Street 2",
    //        "Fake city", "12312", "Fake country");

    //    // Create new user
    //    _authenticationService.SignUp(employee, "test");

    //    var result = _authenticationService.SignIn("John", "test");

    //    Assert.That(result, Is.True);
    //}

    //[Test]
    //public void SignUpAlreadyCreatedUser()
    //{
    //    var employee = CreateEmployee("John", "John", "Doe",
    //        "john@test.com", "1994-12-01", "123123123", "Street 2",
    //        "Fake city", "12312", "Fake country");

    //    _authenticationService.SignUp(employee, "test");
    //    var result = _authenticationService.SignUp(employee, "test");

    //    Assert.That(result, Is.False);
    //}

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

