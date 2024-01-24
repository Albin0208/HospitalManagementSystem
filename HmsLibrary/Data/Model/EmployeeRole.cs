using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HmsLibrary.Data.Model;

public class EmployeeRole : BaseEntity
{
    [Required, MaxLength(40)]
    public string RoleName { get; set; }
    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
}