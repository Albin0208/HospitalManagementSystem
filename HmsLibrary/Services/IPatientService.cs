using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IPatientService
{
    string GetPatientName();
    Task<Patient?> GetPatient(int id);
    /// <summary>
    /// Create a new patient in the database
    /// </summary>
    /// <param name="patient">The patient to be added to the database</param>
    /// <returns>The id of the new patient</returns>
    Task<int> CreatePatient(Patient patient);
}