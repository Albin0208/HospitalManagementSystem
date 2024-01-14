using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace HMSTests;

public class PatientTests
{
    private HmsDbContext _dbContext;
    private IPatientService _patientService;

    [SetUp]
    public void Setup()
    {
        // Setup database connection
        var options = new DbContextOptionsBuilder<HmsDbContext>()
            .UseInMemoryDatabase(databaseName: "HmsDb")
            .Options;

        _dbContext = new HmsDbContext(options);

        // Setup service
        _patientService = new PatientService(_dbContext);
    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    [Test]
    public async Task InsertPatientAsync()
    {
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
        };

        var createdPatient = await _patientService.CreatePatient(patient);

        Assert.That(createdPatient, Is.Not.Null);
        Assert.That(createdPatient.Id, Is.GreaterThan(0));
        Assert.Multiple(() =>
        {
            Assert.That(createdPatient.FirstName, Is.EqualTo(patient.FirstName));
            Assert.That(createdPatient.LastName, Is.EqualTo(patient.LastName));
        });
    }

    [Test]
    public async Task GetPatientAsync()
    {
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
        };

        var createdPatient = await _patientService.CreatePatient(patient);

        var insertedPatient = await _patientService.GetPatient(createdPatient.Id);
        Assert.That(insertedPatient, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(insertedPatient.FirstName, Is.EqualTo(patient.FirstName));
            Assert.That(insertedPatient.LastName, Is.EqualTo(patient.LastName));
        });
    }
}