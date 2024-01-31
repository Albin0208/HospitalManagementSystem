using HmsAPI.DTO;
using HmsAPI.DTO.ResponseDTO;
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
        // Lookup the employeeRole and check if it exists
        var role = await _roleService.GetRole(request.RoleId);

        if (role == null)
        {
            return BadRequest("EmployeeRole does not exist");
        }

        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            RoleId = role.Id,
            Role = role,
            Password = "1234"
        };

        employee = await _employeeService.CreateEmployee(employee);

        var response = EmployeeResponse.FromEmployee(employee);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetEmployees();

        var response = employees.Select(EmployeeResponse.FromEmployee);

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _employeeService.GetEmployee(id);

        if (employee == null)
        {
            return NotFound();
        }

        var response = EmployeeResponse.FromEmployee(employee);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        try
        {
            var employee = await _employeeService.DeleteEmployee(id);

            var response = EmployeeResponse.FromEmployee(employee);
            return Ok(response);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}