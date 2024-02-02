using HmsLibrary.Data.DTO;
using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public interface IAppointmentService
{
    Task<Appointment> CreateAppointment(AppointmentDto appointment);
    Task<Appointment?> GetAppointment(Guid id);
    Task<List<Appointment>> GetAllAppointments();
    Task<List<Appointment>> GetAppointmentsByCriteria(DateTime? date, Guid? doctorId, Guid? patientId);
    Task<Appointment> DeleteAppointment(Guid id);
    Task<Appointment> UpdateAppointment(AppointmentDto appointment);
}