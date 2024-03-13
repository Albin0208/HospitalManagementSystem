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
using static System.Runtime.InteropServices.JavaScript.JSType;

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

    public async Task<IdentityResult> CreateEmployee(CreateEmployeeRequest request)
    {
        ArgumentNullException.ThrowIfNull(request.Employee);

        var employee = request.Employee;

        if (string.IsNullOrWhiteSpace(employee.FirstName))
        {
            throw new ArgumentException("Firstname cannot be empty or null.", nameof(employee.FirstName));
        }

        if (string.IsNullOrWhiteSpace(employee.LastName))
        {
            throw new ArgumentException("Lastname cannot be empty or null.", nameof(employee.LastName));
        }

        // Setup a new user with login and password
        var user = new ApplicationUser
        {
            UserName = employee.Email,
            Email = employee.Email
        };

        var result = await _userManager.CreateAsync(user, request.Password);


        if (!result.Succeeded)
        {
            return result;
        }

        employee.Id = user.Id;
        employee.Username = user.UserName;

        try
        {
            // Add the user to the employee role
            // TODO Fix so the role is not hardcoded
            // TODO Also go thorugh the list of roles and add all the roles to the user
            await _userManager.AddToRoleAsync(user, "Employee");
            await _dbContext.Employees.AddAsync(employee);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            // Remove the user if the employee creation fails
            await _userManager.DeleteAsync(user);

            // Create a failed IdentityResult instance with the error details
            var error = new IdentityError { Code = "UsernameErros", Description = "Error creating employee: " + e.Message };
            var errorList = new List<IdentityError> { error };
            var identityResult = IdentityResult.Failed([.. errorList]);

            return identityResult;
        }

        return result;
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
}