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
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("register/patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterRequest request)
        {
            var result = await _authenticationService.RegisterPatient(request);

            if (result.Succeeded)
            {
                return Created("result", result);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("register/employee")]
        public async Task<IActionResult> RegisterEmployee([FromBody] RegisterRequest request)
        {
            var result = await _authenticationService.RegisterEmployee(request);

            if (result.Succeeded)
            {
                return Created("result", result);
            }

            return BadRequest(result.Errors);
        }
    }
}
