using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Services.EmployeeServices;

public class EmployeeService : IEmployeeService
{
    private readonly HmsDbContext _dbContext;
    private readonly IRoleService _roleService;
    public EmployeeService(HmsDbContext dbContext, IRoleService roleService)
    {
        _dbContext = dbContext;
        _roleService = roleService;
    }

    public Task<List<Employee>> GetEmployees()
    {
        return _dbContext.Employees.Include(e => e.Role).ToListAsync();
    }

    public Task<Employee?> GetEmployee(Guid id)
    {
        return _dbContext.Employees.Include(e => e.Role).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<Employee> CreateEmployee(Employee employee)
    {
        ArgumentNullException.ThrowIfNull(employee);

        if (string.IsNullOrWhiteSpace(employee.FirstName))
        {
            throw new ArgumentException("Firstname cannot be empty or null.", nameof(employee.FirstName));
        }

        if (string.IsNullOrWhiteSpace(employee.LastName))
        {
            throw new ArgumentException("Lastname cannot be empty or null.", nameof(employee.LastName));
        }

        // Retrieve the employeeRole from the database based on RoleId
        var role = await _roleService.GetRole(employee.RoleId) ?? throw new ArgumentException($"Role with ID {employee.RoleId} not found.", nameof(employee.RoleId));
        employee.Role = role;

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> UpdateEmployee(Employee employee)
    {
        var existingEmployee = await _dbContext.Employees.FindAsync(employee.Id) ?? throw new ArgumentException($"Employee with ID {employee.Id} not found.", nameof(employee.Id));

        var properties = typeof(Employee).GetProperties();

        foreach (var property in properties)
        {
            var newValue = property.GetValue(employee);

            // If role is to update check if it exists
            if (property.Name == "RoleId" && newValue != null && (Guid)newValue != Guid.Empty)
            {
                var role = await _roleService.GetRole((Guid)newValue) ?? throw new ArgumentException($"Role with ID {newValue} not found.", nameof(newValue));
                existingEmployee.Role = role;
                continue;
            }

            if (newValue != null)
            {
                property.SetValue(existingEmployee, newValue);
            }
        }


        await _dbContext.SaveChangesAsync();

        return existingEmployee;
    }

    public async Task<Employee> DeleteEmployee(Guid id)
    {
        var employee = await _dbContext.Employees.FindAsync(id) ?? throw new ArgumentException($"Employee with ID {id} not found.", nameof(id));
        _dbContext.Employees.Remove(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }
}