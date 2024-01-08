using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IPatientService
{
    string GetPatientName();
    Task<Patient?> GetPatient(int id);
    void CreatePatient(string name);
}