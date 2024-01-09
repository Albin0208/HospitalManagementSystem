using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace HMSTests;

public class UserTest
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
    public void Test1()
    {
        Assert.Pass();
    }
}