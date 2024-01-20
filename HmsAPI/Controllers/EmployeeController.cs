using HmsAPI.DTO;
using HmsLibrary.Data.Model;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HmsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest request)
    {
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role,
        };

        employee = await _employeeService.CreateEmployee(employee);

        return Ok(employee);
    }
}