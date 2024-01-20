using HmsLibrary.Data.Model;

namespace HmsLibrary.Services.EmployeeServices;

public interface IEmployeeService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(int id);
    Task<Employee> CreateEmployee(Employee employee);
}