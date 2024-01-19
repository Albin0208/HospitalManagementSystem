using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IAppointmentService
{
    Task<Appointment> CreateAppointment(Appointment appointment);
    Task<Appointment?> GetAppointment(int id);
    Task<List<Appointment>> GetAllAppointments();
    Task<List<Appointment>> GetAppointmentsByCriteria(DateTime? date, int? doctorId, int? patientId);
}