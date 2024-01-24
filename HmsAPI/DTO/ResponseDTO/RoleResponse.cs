using HmsLibrary.Data.Model;

namespace HmsAPI.DTO.ResponseDTO;

public class RoleResponse
{
    public int Id { get; set; }
    public string RoleName { get; set; } = "";

    public static RoleResponse FromRole(EmployeeRole role)
    {
        return new RoleResponse
        {
            Id = role.Id,
            RoleName = role.RoleName,
        };
    }
}