using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.Model;

public class Employee : BaseEntity
{
    [Required, MaxLength(30)]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string Role { get; set; } = "Employee";
    [Required, MaxLength(30)]
    public string FirstName { get; set; }
    [Required, MaxLength(30)]
    public string LastName { get; set; }
    [EmailAddress, MaxLength(50)]
    public string? Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    [MaxLength(100)]
    public string? Address { get; set; }
    [MaxLength(50)]
    public string? City { get; set; }
    [MaxLength(10)]
    public string? ZipCode { get; set; }
    [MaxLength(50)]
    public string? Country { get; set; }
}
