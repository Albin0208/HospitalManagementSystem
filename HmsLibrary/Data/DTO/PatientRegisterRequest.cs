using Microsoft.AspNetCore.Identity.Data;

namespace HmsAPI.DTO.RequestDTO
{
    public class PatientRegisterRequest
    {
        public required string Email { get; init; }
        public required string Password { get; init; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
