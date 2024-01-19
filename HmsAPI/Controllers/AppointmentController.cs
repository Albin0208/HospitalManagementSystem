using Microsoft.AspNetCore.Mvc;
namespace HmsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment()
    {
        throw new NotImplementedException();
    }
}