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
    public EmployeeService(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<Employee>> GetEmployees()
    {
        return _dbContext.Employees.ToListAsync();
    }

    public Task<Employee?> GetEmployee(int id)
    {
        return _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == id);
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

        await _dbContext.Employees.AddAsync(employee);
        await _dbContext.SaveChangesAsync();

        return employee;
    }
}