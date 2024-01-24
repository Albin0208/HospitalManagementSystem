using HmsAPI.DTO;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.AspNetCore.Mvc;

namespace HmsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly IRoleService _roleService;

    public EmployeeController(IEmployeeService employeeService, IRoleService roleService)
    {
        _employeeService = employeeService;
        _roleService = roleService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeRequest request)
    {
        // Lookup the role and check if it exists
        var role = await _roleService.GetRole(request.RoleId);

        if (role == null)
        {
            return BadRequest("Role does not exist");
        }

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Role = role,
            Password = "1234"
        };

        employee = await _employeeService.CreateEmployee(employee);

        return Ok(employee);
    }
}