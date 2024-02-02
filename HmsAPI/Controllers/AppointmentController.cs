using HmsAPI.DTO;
using HmsAPI.DTO.ResponseDTO;
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
    public async Task<IActionResult> GetAppointment(Guid id)
    {
        var appointment = await _appointmentService.GetAppointment(id);

        if (appointment == null)
        {
            return NotFound();
        }

        var result = AppointmentResponse.FromAppointment(appointment);

        return Ok(result);
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

        var createdAppointment = await _appointmentService.CreateAppointment(appointment);

        var result = AppointmentResponse.FromAppointment(createdAppointment);

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAppointments()
    {
        var appointments = await _appointmentService.GetAllAppointments();

        var result = appointments.Select(AppointmentResponse.FromAppointment);

        return Ok(result);
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
            var appointmentList = await _appointmentService.GetAppointmentsByCriteria(date, doctorId, patientId);

            var result = appointmentList.Select(AppointmentResponse.FromAppointment);

            return Ok(result);
        }
        catch (Exception)
        {
            // Log the exception or handle it based on your application's requirements
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAppointment(Guid id)
    {
        try
        {
            var appointment = await _appointmentService.DeleteAppointment(id);

            var result = AppointmentResponse.FromAppointment(appointment);

            return Ok(result);
        }
        catch (Exception)
        {
            // Log the exception or handle it based on your application's requirements
            return StatusCode(500, "Internal Server Error");
        }
    }
}