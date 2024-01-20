using HmsAPI.DTO;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HmsAPI.Controllers
{
    /// <summary>
    /// Controller for handling patient requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;

        /// <summary>
        /// Initialize a new instance of the <see cref="PatientController"/> class
        /// </summary>
        /// <param name="patientService">The patientservice that handles the logic</param>
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <summary>
        /// Get all patients
        /// </summary>
        /// <returns>The list of patients</returns>
        [HttpGet]
        public async Task<IActionResult> GetPatientsAsync()
        {
            var patients = await _patientService.GetPatients();

            return Ok(patients);
        }

        /// <summary>
        /// Get a specific patient by their ID
        /// </summary>
        /// <param name="id">The ID of the patient to retrieve</param>
        /// <returns>The patient with the ID</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient(int id)
        {
            var patient = await _patientService.GetPatient(id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        /// <summary>
        /// Creates a new patient.
        /// </summary>
        /// <param name="patient">The patient information to create.</param>
        /// <returns>The created patient.</returns>
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientRequest request)
        {
            var patient = new Patient
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Address = request.Address,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                ZipCode = request.ZipCode,
                City = request.City,
                Country = request.Country,
            };

            var createdPatient = await _patientService.CreatePatient(patient);

            return Created("patient", createdPatient);
        }
    }
}
