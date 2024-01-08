using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HMSTests
{
    public class Tests
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
        }

        [Test]
        public void Test1()
        {
            var patient = new Patient
            {
                FirstName = "John",
                LastName = "Doe",
            };

            _dbContext.Patients.Add(patient);
            _dbContext.SaveChanges();

            var insertedPatient = _dbContext.Patients.Find(patient.Id);
            Assert.That(insertedPatient, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(insertedPatient.FirstName, Is.EqualTo(patient.FirstName));
                Assert.That(insertedPatient.LastName, Is.EqualTo(patient.LastName));
            });
        }
    }
}