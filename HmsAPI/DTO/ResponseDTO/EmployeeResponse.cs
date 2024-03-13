using HmsLibrary.Data.Model;

namespace HmsAPI.DTO.ResponseDTO;

public class EmployeeResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; } = "";
    public List<string> Roles { get; set; } = [];
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public string? Email { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public string? ZipCode { get; set; }
    public string? Country { get; set; }

    public static EmployeeResponse FromEmployee(Employee employee)
    {
        if (employee == null)
        {
            return new EmployeeResponse();
        }

        return new EmployeeResponse
        {
            Id = employee.Id,
            Username = employee.Username,
            //Role = employee.Role?.RoleName,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Email = employee.Email,
            DateOfBirth = employee.DateOfBirth,
            PhoneNumber = employee.PhoneNumber,
            Address = employee.Address,
            City = employee.City,
            ZipCode = employee.ZipCode,
            Country = employee.Country,
        };
    }

    public static EmployeeResponse FromEmployee(Employee employee, List<string>? Roles)
    {
        if (employee == null)
        {
            return new EmployeeResponse();
        }

        var emp = FromEmployee(employee);

        emp.Roles = Roles ?? new List<string>();

        return emp;
    }
}