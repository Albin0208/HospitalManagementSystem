using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.Model;

public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Username { get; set; } = "";
    [Required]
    public string Password { get; set; } = "";
    public string? FirstName { get; set; } = "";
    public string? LastName { get; set; } = "";
    [EmailAddress]
    public string? Email { get; set; } = "";
    [Phone]
    public string? PhoneNumber { get; set; } = "";
    public string? Address { get; set; } = "";
    public string? City { get; set; } = "";
    public string? State { get; set; } = "";
    public string? ZipCode { get; set; } = "";
    public string? Country { get; set; } = "";
    [Required, Timestamp]
    public DateTime CreatedAt { get; set; }
    [Required, Timestamp]
    public DateTime UpdatedAt { get; set; }
}
