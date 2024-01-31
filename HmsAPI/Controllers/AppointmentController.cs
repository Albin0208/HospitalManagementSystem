using HmsAPI.DTO;
using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;
using Microsoft.AspNetCore.Mvc;
namespace HmsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAppointment(int id)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public async Task<IActionResult> CreateAppointment([FromBody] AppointmentRequest request)
    {
        // Check for all required fields
        if (request.Date == default || request.DoctorId == default || request.PatientId == default)
        {
            return BadRequest("Required fields are missing");
        }

        var appointment = new AppointmentDTO
        {
            Date = request.Date,
            DoctorId = request.DoctorId,
            PatientId = request.PatientId,
            Reason = request.Reason,
            Notes = request.Notes
        };

        var result = await _appointmentService.CreateAppointment(appointment);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAppointments()
    {
        throw new NotImplementedException();
    }

    [HttpGet("search")]
    public async Task<IActionResult> GetAppointmentsByCriteria(
        [FromQuery] DateTime? date,
        [FromQuery(Name = "doctorId")] Guid? doctorId,
        [FromQuery(Name = "patientId")] Guid? patientId)
    {
        try
        {
            // Your logic for fetching appointments based on criteria
            var result = await _appointmentService.GetAppointmentsByCriteria(date, doctorId, patientId);

            return Ok(result);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it based on your application's requirements
            return StatusCode(500, "Internal Server Error");
        }
    }
}