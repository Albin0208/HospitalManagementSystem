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

    /// <inheritdoc />
    public async Task<List<IdentityRole<Guid>>> GetRoles() => await _roleManager.Roles.ToListAsync();

    /// <inheritdoc />
    public async Task<IdentityRole<Guid>?> GetRole(Guid id) => await _roleManager.FindByIdAsync(id.ToString());

    /// <inheritdoc />
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

        return role;
    }

    /// <inheritdoc />
    public Task<List<IdentityRole<Guid>>> GetRoles(List<Guid> roleIds) => _roleManager.Roles.Where(r => roleIds.Contains(r.Id)).ToListAsync();
}