using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace HmsAPI.DTO.ResponseDTO;

public class RoleResponse
{
    public Guid Id { get; set; }
    public string RoleName { get; set; } = "";

    public static RoleResponse FromRole(IdentityRole<Guid> role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            RoleName = role.Name,
        };
    }
}