using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IRoleService
{
    Task<List<EmployeeRole>> GetRoles();
    Task<EmployeeRole?> GetRole(int id);
    Task<EmployeeRole> CreateRole(EmployeeRole employeeRole);
}