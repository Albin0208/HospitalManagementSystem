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

    public Task<List<Role>> GetRoles()
    {
        return _dbContext.Roles.ToListAsync();
    }

    public Task<Role?> GetRole(int id)
    {
        return _dbContext.Roles.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Role> CreateRole(Role role)
    {
        ArgumentNullException.ThrowIfNull(role);

        if (string.IsNullOrWhiteSpace(role.RoleName))
        {
            throw new ArgumentException("Role name cannot be empty or null.", nameof(role.RoleName));
        }

        await _dbContext.Roles.AddAsync(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }
}