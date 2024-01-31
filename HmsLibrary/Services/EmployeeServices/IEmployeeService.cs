using HmsLibrary.Data.Model;

namespace HmsLibrary.Services.EmployeeServices;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(Guid id);
    Task<Employee> CreateEmployee(Employee employee);
    Task<Employee> UpdateEmployee(Employee employee);
    Task<Employee> DeleteEmployee(Guid id);
}