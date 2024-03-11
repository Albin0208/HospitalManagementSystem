using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsAPI.Data;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Services;

public class RoleService : IRoleService
{
    private readonly HmsDbContext _dbContext;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleService(HmsDbContext dbContext, RoleManager<IdentityRole<Guid>> roleManager)
    {
        _dbContext = dbContext;
        _roleManager = roleManager;
    }

    public Task<List<EmployeeRole>> GetRoles()
    {
        return _dbContext.EmployeeRoles.ToListAsync();
    }

    public Task<EmployeeRole?> GetRole(Guid id)
    {
        return _dbContext.EmployeeRoles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<EmployeeRole> CreateRole(EmployeeRole employeeRole)
    {
        ArgumentNullException.ThrowIfNull(employeeRole);

        if (string.IsNullOrWhiteSpace(employeeRole.RoleName))
        {
            throw new ArgumentException("EmployeeRole name cannot be empty or null.", nameof(employeeRole.RoleName));
        }

        // Check if role already exists
        if (await _dbContext.EmployeeRoles.AnyAsync(r => r.RoleName == employeeRole.RoleName))
        {
            throw new ArgumentException($"EmployeeRole with name {employeeRole.RoleName} already exists.", nameof(employeeRole.RoleName));
        }

        var role = employeeRole.RoleName;

        if (await _roleManager.RoleExistsAsync(role))
        {
            throw new ArgumentException($"EmployeeRole with name {role} already exists.", nameof(employeeRole.RoleName));
        }
        //_roleManager.CreateAsync(new IdentityRole(role));

        await _dbContext.EmployeeRoles.AddAsync(employeeRole);
        await _dbContext.SaveChangesAsync();

        return employeeRole;
    }

    /// <inheritdoc />
    public async Task<EmployeeRole> DeleteRole(Guid id)
    {
        var role = await _dbContext.EmployeeRoles.FindAsync(id) ?? throw new ArgumentException($"Role with ID {id} not found.", nameof(id));

        _dbContext.EmployeeRoles.Remove(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }
}