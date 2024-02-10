using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace HmsLibrary.Services;

public interface IAuthenticationService
{
    bool SignIn(string username, string password);
    bool SignUp(Employee employee, string password);
    Task<IdentityResult> RegisterPatient(PatientRegisterRequest request);
    Task<IdentityResult> RegisterEmployee(RegisterRequest request);
}