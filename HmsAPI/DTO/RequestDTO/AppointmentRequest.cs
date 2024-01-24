namespace HmsAPI.DTO;

public class AppointmentRequest
{
    public DateTime Date { get; set; }
    public int DoctorId { get; set; }
    public int PatientId { get; set; }
    public string Reason { get; set; }
    public string? Notes { get; set; }
}