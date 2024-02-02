namespace HmsAPI.DTO;

public class AppointmentRequest
{
    public DateTime? Date { get; set; }
    public Guid? DoctorId { get; set; }
    public Guid? PatientId { get; set; }
    public string? Reason { get; set; }
    public string? Notes { get; set; }
}