using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace HmsLibrary.Services;

public interface IRoleService
{
    Task<List<IdentityRole<Guid>>> GetRoles();
    Task<List<IdentityRole<Guid>>> GetRoles(List<Guid> roleIds);
    Task<IdentityRole<Guid>?> GetRole(Guid id);
    Task<IdentityRole<Guid>> CreateRole(string name);
    /// <summary>
    /// Delete a specific role
    /// </summary>
    /// <param name="id">The id of the role</param>
    /// <exception cref="ArgumentException">Throws if the id does not exist</exception>
    /// <returns>The deleted employee role</returns>
    Task<IdentityRole<Guid>> DeleteRole(Guid id);
}