using HmsAPI.DTO.RequestDTO;
using HmsAPI.DTO.ResponseDTO;
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
    [ProducesResponseType<RoleResponse>(200)]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleService.GetRoles();

        var response = roles.Select(RoleResponse.FromRole);

        return Ok(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType<RoleResponse>(200)]
    public async Task<IActionResult> GetRole(Guid id)
    {
        var role = await _roleService.GetRole(id);

        if (role == null)
        {
            return NotFound();
        }

        var response = RoleResponse.FromRole(role);

        return Ok(response);
    }

    [HttpPost]
    [ProducesResponseType<RoleResponse>(200)]
    public async Task<IActionResult> CreateRole([FromBody] RoleRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.RoleName))
        {
            return BadRequest("EmployeeRole name cannot be empty or null.");
        }

        var employeeRole = new EmployeeRole
        {
            RoleName = request.RoleName
        };

        employeeRole = await _roleService.CreateRole(employeeRole);

        var response = RoleResponse.FromRole(employeeRole);

        return Ok(response);
    }

    /// <summary>
    /// Delete a role
    /// </summary>
    /// <param name="id">The guid of the role to be deleted</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType<RoleResponse>(200)]
    public async Task<IActionResult> DeleteRole(Guid id)
    {
        var role = await _roleService.DeleteRole(id);

        var response = RoleResponse.FromRole(role);

        return Ok(response);
    }

}