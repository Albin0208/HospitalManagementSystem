using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace HmsLibrary.Services;

public interface IRoleService
{
    /// <summary>
    /// Fetch all the roles
    /// </summary>
    /// <returns>A list of roles</returns>
    Task<List<IdentityRole<Guid>>> GetRoles();
    Task<List<IdentityRole<Guid>>> GetRoles(List<Guid> roleIds);
    /// <summary>
    /// Get a specific role by its id
    /// </summary>
    /// <param name="id">The id for the role</param>
    /// <returns>The matched role or null</returns>
    Task<IdentityRole<Guid>?> GetRole(Guid id);
    /// <summary>
    /// Create a new role with a specific name
    /// </summary>
    /// <param name="name">The name of the role</param>
    /// <returns>The newly created role</returns>
    Task<IdentityRole<Guid>> CreateRole(string name);
    /// <summary>
    /// Delete a specific role
    /// </summary>
    /// <param name="id">The id of the role</param>
    /// <exception cref="ArgumentException">Throws if the id does not exist</exception>
    /// <returns>The deleted employee role</returns>
    Task<IdentityRole<Guid>> DeleteRole(Guid id);
}