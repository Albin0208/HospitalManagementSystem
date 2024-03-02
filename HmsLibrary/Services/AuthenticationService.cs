using HmsAPI.Data;
using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.DTO;
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

    public async Task<AuthResponse?> SignIn(string username, string password)
    {
        var result = await _signInManager.PasswordSignInAsync(username, password, false, false);

        if (result.Succeeded)
        {
            var user = await _userManager.FindByNameAsync(username);
            var accessToken = GenerateAccessToken(user!);
            var refreshToken = GenerateRefreshToken(user!);
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }

        return null;
    }

    private static string GenerateAccessToken(ApplicationUser user)
    {
        // TODO Fetch the accesstoken secret from appsettings.json or similar
        var accessTokenSecret = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU=";

        return GenerateJwtToken(user, accessTokenSecret);
    }

    private static string GenerateRefreshToken(ApplicationUser user)
    {
        // TODO Fetch the refreshtoken secret from appsettings.json or similar
        var refreshTokenSecret = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU=";

        return GenerateJwtToken(user, refreshTokenSecret);
    }

    private static bool ValidateAccessToken(string token)
    {
        // TODO Fetch the accesstoken secret from appsettings.json or similar
        var accessTokenSecret = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU=";
        return ValidateJwtToken(token, accessTokenSecret).Result;
    }

    private static bool ValidateRefreshToken(string token)
    {
        // TODO Fetch the refreshtoken secret from appsettings.json or similar
        var refreshTokenSecret = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU=";
        return ValidateJwtToken(token, refreshTokenSecret).Result;
    }

    /// <summary>
    /// Generates a JWT token for the given user
    /// </summary>
    /// <param name="user">The user we want a token for</param>
    /// <param name="secretKey">The secret used for generating the token</param>
    /// <returns>The JWT token</returns>
    private static string GenerateJwtToken(ApplicationUser user, string secretKey)
    {
        //var secretKey = "D/X4yFrh3i1po3MV4DEOdSIeuuii8Hji28bqMBtPwmU="; // TODO Remove this and add to appsettings.json or similar

        // Define token parameters
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

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

    public static async Task<bool> ValidateJwtToken(string token, string secretKey)
    {
        var tokenHandler = new JsonWebTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);

        try
        {
            await tokenHandler.ValidateTokenAsync(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            });
        }
        catch
        {
            return false;
        }

        return true;
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
