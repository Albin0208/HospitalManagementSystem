using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IPatientService
{
    /// <summary>
    /// Get the patient with the given id
    /// </summary>
    /// <param name="id">The patient id</param>
    /// <returns>The patient if they exist</returns>
    Task<Patient?> GetPatient(Guid id);
    /// <summary>
    /// Create a new patient in the database
    /// </summary>
    /// <param name="patient">The patient to be added to the database</param>
    /// <returns>The id of the new patient</returns>
    Task<Patient> CreatePatient(Patient patient);

    /// <summary>
    /// Get all patients in the database
    /// </summary>
    /// <returns>All patients in the database</returns>
    Task<List<Patient>> GetPatients();

    /// <summary>
    /// Update the given patient in the database
    /// </summary>
    /// <param name="patient">The patient with the modified fields</param>
    /// <returns>The modified patient</returns>
    Task<Patient> UpdatePatient(Patient patient);
}