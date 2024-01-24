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

    public Task<EmployeeRole?> GetRole(int id)
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

        await _dbContext.Roles.AddAsync(employeeRole);
        await _dbContext.SaveChangesAsync();

        return employeeRole;
    }
}