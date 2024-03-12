using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HmsLibrary.Data.DTO;

public class RoleResponse
{
    public Guid Id { get; set; }
    public string RoleName { get; set; }

    public static RoleResponse FromRole(IdentityRole<Guid> role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            RoleName = role.Name
        };
    }
}
