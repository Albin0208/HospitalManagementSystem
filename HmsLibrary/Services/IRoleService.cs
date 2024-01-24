using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IRoleService
{
    Task<List<Role>> GetRoles();
    Task<Role?> GetRole(int id);
    Task<Role> CreateRole(Role role);
}