using Microsoft.AspNetCore.Identity.Data;

namespace HmsAPI.DTO.RequestDTO
{
    public class PatientRegisterRequest
    {
        public RegisterRequest RegisterRequest { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
