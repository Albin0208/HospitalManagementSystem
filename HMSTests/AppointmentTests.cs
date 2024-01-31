using HmsLibrary.Data.Context;
using HmsLibrary.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;
using HmsLibrary.Data.Model.Employees;

namespace HMSTests;

public class AppointmentTests
{
    private HmsDbContext _dbContext;
    private IAppointmentService _appointmentService;

    private Doctor doctor;
    private Patient patient;

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
        _appointmentService = new AppointmentService(_dbContext);

        // Create a doctor and a patient
        doctor = new Doctor
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

        patient = new Patient
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        _dbContext.Employees.Add(doctor);
        _dbContext.Patients.Add(patient);
        _dbContext.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        // Tear down database connection
        _dbContext.Dispose();
    }

    [Test]
    public async Task CreateAppointmentAsync()
    {


        var appointment = new AppointmentDTO
        {
            Date = DateTime.Now,
            DoctorId = doctor.Id,
            PatientId = patient.Id,
        };

        var createdAppointment = await _appointmentService.CreateAppointment(appointment);

        Assert.That(createdAppointment, Is.Not.Null);
        //Assert.That(createdAppointment.Id, Is.GreaterThan(0));
        Assert.Multiple(() =>
        {
            Assert.That(createdAppointment.Date, Is.EqualTo(appointment.Date));
            Assert.That(createdAppointment.Doctor, Is.EqualTo(doctor));
            Assert.That(createdAppointment.Patient, Is.EqualTo(patient));
        });
    }

    [Test]
    public Task CreateAppointmentAsync_ThrowsArgumentException()
    {
        var appointment = new AppointmentDTO
        {
            Date = DateTime.Now,
        };

        Assert.ThrowsAsync<ArgumentException>(async () => await _appointmentService.CreateAppointment(appointment));

        return Task.CompletedTask;
    }

    [Test]
    [TestCase("2021-01-01", 1, 1)]
    [TestCase("2021-01-01", null, null)]
    [TestCase(null, 1 , null)]
    [TestCase(null, null, 1)]
    [TestCase(null, null, null)]
    public async Task GetAppointmentsByCriteriaAsync(string? date, int? doctorId, int? patientId)
    {
        var parsedDate = date == null ? default : DateTime.Parse(date);

        var appointment = new Appointment
        {
            Date = parsedDate,
            Doctor = doctor,
            Patient = patient,
        };

        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();

        var passedDoctorId = doctorId.HasValue ? doctor.Id : Guid.Empty;
        var passedPatientId = patientId.HasValue ? patient.Id : Guid.Empty;

        DateTime? d = date == null ? null : DateTime.Parse(date);

        bool a = passedPatientId == Guid.Empty;

        var fetchedAppointments = await _appointmentService.GetAppointmentsByCriteria(d, passedDoctorId, passedPatientId);

        Assert.That(fetchedAppointments, Is.Not.Null);
        Assert.That(fetchedAppointments, Has.Count.EqualTo(1));
    }

    [Test]
    [TestCase("2021-01-01", 1, 1, 1)]
    [TestCase("2021-01-01", null, null, 3)]
    [TestCase("2021-01-01", null, 2, 2)]
    [TestCase("2021-02-01", null, null, 1)]
    [TestCase(null, 1, null, 2)]
    [TestCase(null, null, 1, 2)]
    [TestCase(null, 1, 1, 1)]
    [TestCase(null, null, null, 4)]
    public async Task GetMultipleAppointmentsByCriteria(DateTime? date, int? doctorId, int? patientId, int expectedCount)
    {
        // Create a second doctor and patient
        var doctor2 = new Doctor
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

        var patient2 = new Patient
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith",
            DateOfBirth = DateTime.Parse("1994-12-01"),
            PhoneNumber = "123123123",
            Address = "Street 2",
            City = "Fake city",
            ZipCode = "12312",
            Country = "Fake country",
        };

        await _dbContext.Employees.AddAsync(doctor2);
        await _dbContext.Patients.AddAsync(patient2);
        await _dbContext.SaveChangesAsync();

        // Create a few appointments

        var appointment1 = new Appointment
        {
            Date = DateTime.Parse("2021-01-01"),
            Doctor = doctor,
            Patient = patient,
        };

        var appointment2 = new Appointment
        {
            Date = DateTime.Parse("2021-01-01"),
            Doctor = doctor,
            Patient = patient2,
        };

        var appointment3 = new Appointment
        {
            Date = DateTime.Parse("2021-02-01"),
            Doctor = doctor2,
            Patient = patient,
        };

        var appointment4 = new Appointment
        {
            Date = DateTime.Parse("2021-01-01"),
            Doctor = doctor2,
            Patient = patient2,
        };

        await _dbContext.Appointments.AddRangeAsync(appointment1, appointment2, appointment3, appointment4);
        await _dbContext.SaveChangesAsync();

        var passedDoctorId = Guid.Empty;
        var passedPatientId = Guid.Empty;

        if (doctorId.HasValue)
        {
            passedDoctorId = doctorId.Value == 1 ? doctor.Id : doctor2.Id;
        }

        if (patientId.HasValue)
        {
            passedPatientId = patientId.Value == 1 ? patient.Id : patient2.Id;
        }

        // Fetch appointments by criteria
        var fetchedAppointments = await _appointmentService.GetAppointmentsByCriteria(date, passedDoctorId, passedPatientId);

        Assert.That(fetchedAppointments, Is.Not.Null);
        Assert.That(fetchedAppointments, Has.Count.EqualTo(expectedCount));
    }

    [Test]
    [TestCase("2021-01-01", 1, 1)]
    [TestCase("2021-01-01", null, null)]
    [TestCase(null, 1, null)]
    [TestCase(null, null, 1)]
    [TestCase(null, null, null)]
    public async Task GetAppointmentsByCriteriaAsync_NoAppointments(DateTime? date, int? doctorId, int? patientId)
    {
        var passedDoctorId = doctorId.HasValue ? doctor.Id : Guid.Empty;
        var passedPatientId = patientId.HasValue ? patient.Id : Guid.Empty;

        var fetchedAppointments = await _appointmentService.GetAppointmentsByCriteria(date, passedDoctorId, passedPatientId);

        Assert.That(fetchedAppointments, Is.Not.Null);
        Assert.That(fetchedAppointments, Has.Count.EqualTo(0));
    }


    [Test]
    public async Task GetAppointmentAsync()
    {
        var appointment = new Appointment
        {
            Date = DateTime.Now,
            Doctor = doctor,
            Patient = patient,
        };

        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();

        var fetchedAppointment = await _appointmentService.GetAppointment(appointment.Id);

        Assert.That(fetchedAppointment, Is.Not.Null);
        Assert.That(fetchedAppointment, Is.EqualTo(appointment));
    }

    [Test]
    public async Task GetAppointmentAsync_InvalidId()
    {
        var fetchedAppointment = await _appointmentService.GetAppointment(new Guid());

        Assert.That(fetchedAppointment, Is.Null);
    }

    [Test]
    public async Task GetAppointmentsAsync()
    {
        var appointment1 = new Appointment
        {
            Date = DateTime.Now,
            Doctor = doctor,
            Patient = patient,
        };

        var appointment2 = new Appointment
        {
            Date = DateTime.Now,
            Doctor = doctor,
            Patient = patient,
        };

        await _dbContext.Appointments.AddRangeAsync(appointment1, appointment2);
        await _dbContext.SaveChangesAsync();

        var fetchedAppointments = await _appointmentService.GetAllAppointments();

        Assert.That(fetchedAppointments, Is.Not.Null);
        Assert.That(fetchedAppointments, Has.Count.EqualTo(2));
    }

    [Test]
    public void DeleteAppointment()
    {
        var appointment = new Appointment
        {
            Date = DateTime.Now,
            Doctor = doctor,
            Patient = patient,
        };

        _dbContext.Appointments.Add(appointment);
        _dbContext.SaveChanges();

        var deletedAppointment = _appointmentService.DeleteAppointment(appointment.Id);

        Assert.That(deletedAppointment, Is.Not.Null);
    }

    [Test]
    public void DeleteAppointment_InvalidId()
    {
        Assert.ThrowsAsync<ArgumentException>(async () => await _appointmentService.DeleteAppointment(new Guid()));
    }
}