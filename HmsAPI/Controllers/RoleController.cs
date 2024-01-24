using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HmsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetRoles();

        return Ok(roles);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(int id)
    {
        var role = await _roleService.GetRole(id);

        if (role == null)
        {
            return NotFound();
        }

        return Ok(role);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] EmployeeRole employeeRole)
    {
        if (string.IsNullOrWhiteSpace(employeeRole.RoleName))
        {
            return BadRequest("EmployeeRole name cannot be empty or null.");
        }

        employeeRole = await _roleService.CreateRole(employeeRole);

        return Ok(employeeRole);
    }


}