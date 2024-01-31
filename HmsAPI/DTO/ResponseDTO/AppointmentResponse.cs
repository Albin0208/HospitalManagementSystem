using HmsLibrary.Data.Model;

namespace HmsAPI.DTO.ResponseDTO;

public class AppointmentResponse
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public Guid DoctorId { get; set; }
    public Guid PatientId { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }

    public static AppointmentResponse FromAppointment(Appointment appointment)
    {
        return new AppointmentResponse
        {
            Id = appointment.Id,
            Date = appointment.Date,
            DoctorId = appointment.Doctor.Id,
            PatientId = appointment.Patient.Id,
            Reason = appointment.Reason,
            Notes = appointment.Notes,
        };
    }
}