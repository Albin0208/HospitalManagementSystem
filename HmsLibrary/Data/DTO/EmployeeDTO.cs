using HmsLibrary.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.DTO;

public class EmployeeDTO
{
    public Employee Employee { get; set; }
    public List<string> Roles { get; set; }
}
