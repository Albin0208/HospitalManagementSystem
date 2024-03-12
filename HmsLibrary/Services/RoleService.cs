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

    public async Task<List<IdentityRole<Guid>>> GetRoles()
    {
        // Retrieve roles asynchronously
        var rolesList = await _roleManager.Roles.ToListAsync();

        return rolesList;
    }

    public async Task<IdentityRole<Guid>?> GetRole(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString());

        return role;
    }

    public async Task<IdentityRole<Guid>> CreateRole(string name)
    {
        ArgumentNullException.ThrowIfNull(name);

        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Role name cannot be empty or null.", nameof(name));
        }

        if (await _roleManager.RoleExistsAsync(name))
        {
            throw new ArgumentException($"EmployeeRole with name {name} already exists.", nameof(name));
        }

        var role = new IdentityRole<Guid>(name);

        await _roleManager.CreateAsync(role);

        return role;
    }

    /// <inheritdoc />
    public async Task<IdentityRole<Guid>> DeleteRole(Guid id)
    {
        var role = await _roleManager.FindByIdAsync(id.ToString()) ?? throw new ArgumentException($"Role with ID {id} not found.", nameof(id));

        await _roleManager.DeleteAsync(role);
        await _dbContext.SaveChangesAsync();

        return role;
    }

    public Task<List<IdentityRole<Guid>>> GetRoles(List<Guid> roleIds)
    {
        // Fetch all roles from the database based on the roleIds
        return _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
    }
}