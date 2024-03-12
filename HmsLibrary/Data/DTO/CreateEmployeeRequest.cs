using HmsLibrary.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.DTO;

public class CreateEmployeeRequest
{
    public required Employee Employee { get; set; }
    public List<Guid>? RoleIds { get; set; }
}
