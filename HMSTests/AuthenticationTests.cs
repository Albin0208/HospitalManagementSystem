using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
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
        var result = _authenticationService.SignUp("test", "test");

        Assert.That(result, Is.True);

        var user = _dbContext.Users.FirstOrDefault(u => u.Username == "test");

        Assert.That(user, Is.Not.Null);

        Assert.Multiple(() =>
        {
            Assert.That(user.Username, Is.EqualTo("test"));
            Assert.That(user.Password, Is.Not.EqualTo("test")); // Check that the password is encrypted
        });
    }

    [Test]
    public void UserSignIn()
    {
        // Create new user
        _authenticationService.SignUp("John", "test");

        var result = _authenticationService.SignIn("John", "test");

        Assert.That(result, Is.True);
    }

    [Test]
    public void SignUpAlreadyCreatedUser()
    {
        _authenticationService.SignUp("John", "test");
        var result = _authenticationService.SignUp("John", "test");

        Assert.That(result, Is.False);
    }
}

