using System.ComponentModel.DataAnnotations;
using HmsLibrary.Data.Model;

namespace HmsAPI.DTO;

public class EmployeeRequest
{
    public string Username { get; set; }
    //public string Password { get; set; }
    public int RoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    //public string? Email { get; set; }
    //public DateTime DateOfBirth { get; set; }
    //public string? PhoneNumber { get; set; }
    //public string? Address { get; set; }
    //public string? City { get; set; }
    //public string? ZipCode { get; set; }
    //public string? Country { get; set; }
}