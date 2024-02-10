using HmsAPI.Data;
using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace HmsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmployeeService _employeeService;
        private readonly IPatientService _patientService;

        public AuthController(UserManager<ApplicationUser> userManager, IEmployeeService employeeService, IPatientService patientService)
        {
            _userManager = userManager;
            _employeeService = employeeService;
            _patientService = patientService;
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterRequest request)
        {
            var result = await RegisterBaseUser(request.RegisterRequest);


            if (!result.Result.Succeeded) return BadRequest(result.Result.Errors);

            // TODO Add a new Patient to the database
            var patient = new Patient
            {
                Id = result.User.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };

            try
            {
                var patientResult = await _patientService.CreatePatient(patient);

                return Created();
            }
            catch (Exception e)
            {
                await _userManager.DeleteAsync(result.User);
                return BadRequest(e.Message);
            }
        }

        [HttpPost("register/employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterRequest request)
        {
            var result = await RegisterBaseUser(request);

            if (result.Result.Succeeded)
            {
                return Created();
            }

            return BadRequest(result.Result.Errors);
        }

        private async Task<RegisterResult> RegisterBaseUser(RegisterRequest request)
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
}
