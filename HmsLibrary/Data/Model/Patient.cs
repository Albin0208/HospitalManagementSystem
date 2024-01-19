using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.Model;

public class Patient : BaseEntity
{
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

    public Patient()
    {
    }

    public Patient(Patient patient)
    {
        Id = patient.Id;
        CreatedAt = patient.CreatedAt;
        UpdatedAt = patient.UpdatedAt;
        FirstName = patient.FirstName;
        LastName = patient.LastName;
        Email = patient.Email;
        DateOfBirth = patient.DateOfBirth;
        PhoneNumber = patient.PhoneNumber;
        Address = patient.Address;
        City = patient.City;
        ZipCode = patient.ZipCode;
        Country = patient.Country;
        PhoneNumber = patient.PhoneNumber;
    }
}
