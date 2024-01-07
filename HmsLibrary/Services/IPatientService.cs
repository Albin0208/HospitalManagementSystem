using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IPatientService
{
    string GetPatientName();
    Patient GetPatient(int id);
}