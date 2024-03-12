using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsAPI.Data;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Services.EmployeeServices;

public class EmployeeService : IEmployeeService
{
    private readonly HmsDbContext _dbContext;
    private readonly IRoleService _roleService;
    private readonly UserManager<ApplicationUser> _userManager;

    public EmployeeService(HmsDbContext dbContext, IRoleService roleService, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _roleService = roleService;
        _userManager = userManager;
    }

    public Task<List<Employee>> GetEmployees()
    {
        // Fetch all employees from the database including their roles with the userManager
        return null;
    }

    public Task<Employee?> GetEmployee(Guid id)
    {
        throw new NotImplementedException("Check if to be implemented");
        //return _dbContext.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> CreateEmployee(CreateEmployeeRequest request)
    {
        return new Employee()
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = ""
        };
        //throw new NotImplementedException("Check if to be implemented");
        //ArgumentNullException.ThrowIfNull(request);

        //var employee = request.Employee;

        //if (string.IsNullOrWhiteSpace(employee.FirstName))
        //{
        //    throw new ArgumentException("Firstname cannot be empty or null.", nameof(employee.FirstName));
        //}

        //if (string.IsNullOrWhiteSpace(employee.LastName))
        //{
        //    throw new ArgumentException("Lastname cannot be empty or null.", nameof(employee.LastName));
        //}

        //// retrive all roles from the ids pased in the employee object
        //var roleIds = request.RoleIds;

        //if (roleIds != null)
        //{
        //    // Fetch all roles from the database based on the roleIds
        //    var roles = await _roleService.GetRoles(roleIds);
        //    foreach ( var role in roles)
        //    {
        //        _userManager.AddToRoleAsync(employee, role.Name);
        //    }
        //}


        ////// Retrieve the employeeRole from the database based on RoleId
        ////var role = await _roleService.GetRole(employee.RoleId) ?? throw new ArgumentException($"Role with ID {employee.RoleId} not found.", nameof(employee.RoleId));
        ////employee.Role = role;

        //await _dbContext.Employees.AddAsync(employee);
        //await _dbContext.SaveChangesAsync();

        //return employee;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        throw new NotImplementedException("Check if to be implemented");
        //var existingEmployee = await _dbContext.Employees.FindAsync(employee.Id) ?? throw new ArgumentException($"Employee with ID {employee.Id} not found.", nameof(employee.Id));

        //var properties = typeof(Employee).GetProperties();

        //foreach (var property in properties)
        //{
        //    var newValue = property.GetValue(employee);

        //    // If role is to update check if it exists
        //    if (property.Name == "RoleId" && newValue != null && (Guid)newValue != Guid.Empty)
        //    {
        //        var role = await _roleService.GetRole((Guid)newValue) ?? throw new ArgumentException($"Role with ID {newValue} not found.", nameof(newValue));
        //        existingEmployee.Role = role;
        //        continue;
        //    }

        //    if (newValue != null)
        //    {
        //        property.SetValue(existingEmployee, newValue);
        //    }
        //}


        //await _dbContext.SaveChangesAsync();

        //return existingEmployee;
    }

    public async Task<Employee> DeleteEmployee(Guid id)
    {
        var employee = await _dbContext.Employees.FindAsync(id) ?? throw new ArgumentException($"Employee with ID {id} not found.", nameof(id));
        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }

    public Task<Employee> CreateEmployee(Employee employee)
    {
        throw new NotImplementedException();
    }
}