using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using Microsoft.EntityFrameworkCore;

namespace HMSTests
{
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

            var id = await _patientService.CreatePatient(patient);

            var insertedPatient = await _dbContext.Patients.FindAsync(id); // Access the db to check if the patient was inserted
            Assert.That(insertedPatient, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(insertedPatient.FirstName, Is.EqualTo(patient.FirstName));
                Assert.That(insertedPatient.LastName, Is.EqualTo(patient.LastName));
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

            var id = await _patientService.CreatePatient(patient);

            var insertedPatient = await _patientService.GetPatient(id);
            Assert.That(insertedPatient, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(insertedPatient.FirstName, Is.EqualTo(patient.FirstName));
                Assert.That(insertedPatient.LastName, Is.EqualTo(patient.LastName));
            });
        }
    }
}