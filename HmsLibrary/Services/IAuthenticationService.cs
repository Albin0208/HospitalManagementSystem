namespace HmsLibrary.Services;

public interface IAuthenticationService
{
    bool SignIn(string username, string password);
    bool SignUp(string username, string password);
}