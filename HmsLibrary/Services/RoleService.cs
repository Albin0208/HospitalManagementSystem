using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Services;

public class RoleService : IRoleService
{
    private readonly HmsDbContext _dbContext;

    public RoleService(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<List<EmployeeRole>> GetRoles()
    {
        return _dbContext.Roles.ToListAsync();
    }

    public Task<EmployeeRole?> GetRole(Guid id)
    {
        return _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<EmployeeRole> CreateRole(EmployeeRole employeeRole)
    {
        ArgumentNullException.ThrowIfNull(employeeRole);

        if (string.IsNullOrWhiteSpace(employeeRole.RoleName))
        {
            throw new ArgumentException("EmployeeRole name cannot be empty or null.", nameof(employeeRole.RoleName));
        }

        // Check if role already exists
        if (await _dbContext.Roles.AnyAsync(r => r.RoleName == employeeRole.RoleName))
        {
            throw new ArgumentException($"EmployeeRole with name {employeeRole.RoleName} already exists.", nameof(employeeRole.RoleName));
        }

        await _dbContext.Roles.AddAsync(employeeRole);
        await _dbContext.SaveChangesAsync();

        return employeeRole;
    }

    /// <inheritdoc />
    public async Task<EmployeeRole> DeleteRole(Guid id)
    {
        var role = await _dbContext.Roles.FindAsync(id) ?? throw new ArgumentException($"Role with ID {id} not found.", nameof(id));

        _dbContext.Roles.Remove(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }
}