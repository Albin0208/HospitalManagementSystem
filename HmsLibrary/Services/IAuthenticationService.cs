using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IAuthenticationService
{
    bool SignIn(string username, string password);
    bool SignUp(Employee employee, string password);
}