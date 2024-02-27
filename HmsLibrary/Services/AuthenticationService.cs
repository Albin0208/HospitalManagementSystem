using HmsAPI.Data;
using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HmsLibrary.Services;


public class AuthenticationService : IAuthenticationService
{
    private readonly HmsDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IPatientService _patientService;
    private readonly IEmployeeService _employeeService;

    public AuthenticationService(HmsDbContext dbContext, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IPatientService patientService, IEmployeeService employeeService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
        _patientService = patientService;
        _employeeService = employeeService;
    }

    public async Task<string> SignIn(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

        // TODO On success return a JWT token
        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(username);
            var token = GenerateJwtToken(user);
            return token;
        }




        return null;
    }

    private static string GenerateJwtToken(ApplicationUser user)
    {
        var SecretKey = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU="; // TODO Remove this and add to appsettings.json or similar

        // Define token parameters
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(SecretKey);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
                // Add more claims as needed, such as user role, permissions, etc.
            }),
            Expires = DateTime.UtcNow.AddHours(1), // Token expiration time
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                        SecurityAlgorithms.HmacSha256Signature)
        };

        return tokenHandler.CreateToken(tokenDescriptor);
    }

    public async Task<IdentityResult> RegisterEmployee(PatientRegisterRequest request)
    {
        var result = await RegisterBaseUser(request);

        if (result.Result.Succeeded)
        {
            // TODO Add employee specific data to the employee table
        }

        return result.Result;
    }

    public async Task<IdentityResult> RegisterPatient(PatientRegisterRequest request)
    {
        var result = await RegisterBaseUser(new PatientRegisterRequest
        {
            Email = request.Email,
            Password = request.Password
        });

        if (!result.Result.Succeeded) return result.Result;

        // TODO Add patient specific data to the patient table
        var user = result.User;

        var patient = new Patient
        {
            Id = user.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            // TODO Handle the rest of the properties
            //DateOfBirth = request.DateOfBirth,
            //PhoneNumber = request.PhoneNumber,
            //Address = request.Address,
            //City = request.City,
            //ZipCode = request.ZipCode,
            //Country = request.Country,
        };

        try
        {
            var res = await _patientService.CreatePatient(patient);

            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating patient: {e.Message}");
            return IdentityResult.Failed();
        }
    }

    private async Task<RegisterResult> RegisterBaseUser(PatientRegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return new RegisterResult()
        {
            Result = result,
            User = user
        };
    }
}
public class RegisterResult
{
    public IdentityResult Result { get; set; }
    public ApplicationUser User { get; set; }
}
