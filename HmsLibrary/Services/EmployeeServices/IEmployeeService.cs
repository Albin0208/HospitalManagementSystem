using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace HmsLibrary.Services.EmployeeServices;

public interface IEmployeeService
{
    Task<List<EmployeeDTO>> GetEmployees();
    Task<EmployeeDTO?> GetEmployee(Guid id);
    Task<IdentityResult> CreateEmployee(CreateEmployeeRequest employee);
    Task<Employee> UpdateEmployee(Employee employee);
    Task<Employee> DeleteEmployee(Guid id);
}