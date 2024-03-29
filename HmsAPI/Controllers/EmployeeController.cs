﻿using HmsAPI.DTO;
using HmsAPI.DTO.RequestDTO;
using HmsAPI.DTO.ResponseDTO;
using HmsLibrary.Data.DTO;
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
        //var role = await _roleService.GetRole((Guid)request.RoleId);

        //if (role == null)
        //{
        //    return BadRequest("EmployeeRole does not exist");
        //}
        
        var employee = new Employee
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Username = request.Username,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Address = request.Address,
            City = request.City,
            ZipCode = request.ZipCode,
            Country = request.Country,
            DateOfBirth = request.DateOfBirth ?? default,
            //RoleId = role.Id,
            //Role = role,
        };

        var employeeRequest = new CreateEmployeeRequest
        {
            Employee = employee,
            Password = request.Password,
            RoleIds = request.RoleIds
        };

        var res = await _employeeService.CreateEmployee(employeeRequest);

        var response = EmployeeResponse.FromEmployee(employee);

        return Created("Employee", res);
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetEmployees();
        var response = employees.Select(res => EmployeeResponse.FromEmployee(res.Employee, res.Roles));

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(Guid id)
    {
        var res = await _employeeService.GetEmployee(id);

        if (res.Employee == null)
        {
            return NotFound();
        }

        var response = EmployeeResponse.FromEmployee(res.Employee, res.Roles);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
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

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeRequest request)
    {
        // TODO Add some role authorization here only admin can update roles. An employee can only update their own values except for role
        var employee = await _employeeService.GetEmployee(id);

        if (employee == null)
        {
            return NotFound();
        }

        // Get all properties of the request object
        var requestProperties = typeof(EmployeeRequest).GetProperties();

        // Loop through properties and update employee if property is not null
        foreach (var property in requestProperties)
        {
            var requestValue = property.GetValue(request);

            // Check if property value is not null or whitespace
            if (requestValue == null || string.IsNullOrWhiteSpace(requestValue.ToString())) continue;
            var employeeProperty = typeof(Employee).GetProperty(property.Name);

            // Update corresponding employee property
            employeeProperty?.SetValue(employee, requestValue);
        }


        var e = employee.Employee;

        e = await _employeeService.UpdateEmployee(e);

        var response = EmployeeResponse.FromEmployee(e);

        return Ok(response);
    }

    [HttpPost("add-roles/{userId}")]
    public async Task<IActionResult> AddUserToRoles(Guid userId, [FromBody] List<Guid> roleIds)
    {
        try
        {
            var success = await _employeeService.AddUserToRoles(userId, roleIds);

            return Ok(success);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpDelete("remove-roles/{userId}")]
    public async Task<IActionResult> RemoveUserFromRole(Guid userId, [FromBody] List<Guid> roleIds)
    {
        try
        {
            var success = await _employeeService.RemoveUserFromRole(userId, roleIds);

            return Ok(success);
        }
        catch (ArgumentException e)
        {
            return NotFound(e.Message);
        }
    }
}