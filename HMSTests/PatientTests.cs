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
    [TestCase("John", "Doe", "john.doe@email.com", "2000-01-01", "1234567890", "123 Main St", "CityA", "12345", "CountryA")]
    [TestCase("Jane", "Smith", "jane.smith@email.com", "1995-05-15", "9876543210", "456 Oak St", "CityB", "54321", "CountryB")]
    [TestCase("Jane", "Smith", null, null, null, null, null, null, null)] // Optional parameters are null
    public async Task InsertPatientAsync(
        string firstName, string lastName, string? email, string? dateOfBirth, string? phoneNumber,
        string? address, string? city, string? zipCode, string? country)
    {
        var parsedDateOfBirth = dateOfBirth == null ? default : DateTime.Parse(dateOfBirth);

        var patient = new Patient
        {
            FirstName = firstName,
            LastName = lastName,
            Address = address,
            DateOfBirth = parsedDateOfBirth,
            PhoneNumber = phoneNumber,
            Email = email,
            ZipCode = zipCode,
            City = city,
            Country = country,
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

        await _patientService.CreatePatient(patient1);
        await _patientService.CreatePatient(patient2);

        var patients = await _patientService.GetPatients();
        Assert.That(patients, Is.Not.Null);
        Assert.That(patients.Count, Is.EqualTo(2));
    }

    [Test]
    public async Task GetPatientAsync_InvalidId()
    {
        var patient = await _patientService.GetPatient(1);
        Assert.That(patient, Is.Null);
    }

    [Test]
    public async Task GetPatientsAsync_NoPatients()
    {
        var patients = await _patientService.GetPatients();
        Assert.That(patients, Is.Not.Null);
        Assert.That(patients.Count, Is.EqualTo(0));
    }

    [Test]
    public async Task CreatePatientAsync_NullPatient()
    {
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _patientService.CreatePatient(null!));
    }

    [Test]
    public async Task CreatePatientAsync_NullFirstName()
    {
        var patient = new Patient
        {
            FirstName = null!,
            LastName = "Doe",
            Address = "123 Fake St",
            DateOfBirth = new DateTime(2000, 1, 1),
            PhoneNumber = "1234567890",
            Email = ""
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await _patientService.CreatePatient(patient));


        Assert.That(_dbContext.Patients.Count(), Is.EqualTo(0));
    }
}