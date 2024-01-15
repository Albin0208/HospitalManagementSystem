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
        _dbContext.Database.EnsureDeleted(); // Delete database before each test

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
            Address = "123 Fake St",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            Email = "test@test.com",
            ZipCode = "12345",
            City = "Fake City",
            Country = "Fake Country",
        };

        var createdPatient = await _patientService.CreatePatient(patient);

        Assert.That(createdPatient, Is.Not.Null);
        Assert.That(createdPatient.Id, Is.GreaterThan(0));
        Assert.Multiple(() =>
        {
            Assert.That(createdPatient.FirstName, Is.EqualTo(patient.FirstName));
            Assert.That(createdPatient.LastName, Is.EqualTo(patient.LastName));
            Assert.That(createdPatient.Address, Is.EqualTo(patient.Address));
            Assert.That(createdPatient.DateOfBirth, Is.EqualTo(patient.DateOfBirth));
            Assert.That(createdPatient.PhoneNumber, Is.EqualTo(patient.PhoneNumber));
            Assert.That(createdPatient.Email, Is.EqualTo(patient.Email));
            Assert.That(createdPatient.ZipCode, Is.EqualTo(patient.ZipCode));
            Assert.That(createdPatient.City, Is.EqualTo(patient.City));
            Assert.That(createdPatient.Country, Is.EqualTo(patient.Country));
        });
    }

    [Test]
    public async Task GetPatientAsync()
    {
        var patient = new Patient
        {
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Fake St",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            Email = "test@test.com",
            ZipCode = "12345",
            City = "Fake City",
            Country = "Fake Country",
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

    [Test]
    public async Task GetPatientsAsync()
    {
        var patient1 = new Patient
        {
            FirstName = "Jake",
            LastName = "Doe",
            Address = "123 Fake St",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            Email = "test@test.com",
            ZipCode = "12345",
            City = "Fake City",
            Country = "Fake Country",
        };

        var patient2 = new Patient
        {
            FirstName = "Jane",
            LastName = "Doe",
            Address = "123 Fake St",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            Email = "test@test.com",
            ZipCode = "12345",
            City = "Fake City",
            Country = "Fake Country",
        };

        var p1 = await _patientService.CreatePatient(patient1);
        var p2 = await _patientService.CreatePatient(patient2);

        var patients = await _patientService.GetPatients();
        Assert.That(patients, Is.Not.Null);
        Assert.That(patients.Count, Is.EqualTo(2));
    }
}