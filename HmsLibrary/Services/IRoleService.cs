using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IRoleService
{
    Task<List<EmployeeRole>> GetRoles();
    Task<EmployeeRole?> GetRole(Guid id);
    Task<EmployeeRole> CreateRole(EmployeeRole employeeRole);
    /// <summary>
    /// Delete a specific role
    /// </summary>
    /// <param name="id">The id of the role</param>
    /// <exception cref="ArgumentException">Throws if the id does not exist</exception>
    /// <returns>The deleted employee role</returns>
    Task<EmployeeRole> DeleteRole(Guid id);
}