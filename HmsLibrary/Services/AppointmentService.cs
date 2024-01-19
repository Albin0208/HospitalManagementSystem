using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Services;

public class AppointmentService : IAppointmentService
{
    private readonly HmsDbContext _dbContext;

    public AppointmentService(HmsDbContext context)
    {
        _dbContext = context;
    }

    public async Task<Appointment> CreateAppointment(Appointment appointment)
    {
        // Validate that the appointment has a doctor and a patient
        if (appointment.Doctor?.Id == null || appointment.Patient?.Id == null)
        {
            throw new ArgumentException("Appointment must have a doctor and a patient", nameof(appointment));
        }

        // Check if the doctor exists
        var doctorExists = await _dbContext.Employees.AnyAsync(e => e.Id == appointment.Doctor.Id);
        if (!doctorExists)
        {
            throw new ArgumentException("Doctor does not exist or is not specified", nameof(appointment));
        }

        // Check if the patient exists
        var patientExists = await _dbContext.Patients.AnyAsync(p => p.Id == appointment.Patient.Id);
        if (!patientExists)
        {
            throw new ArgumentException("Patient does not exist or is not specified", nameof(appointment));
        }

        await _dbContext.Appointments.AddAsync(appointment);
        await _dbContext.SaveChangesAsync();

        return appointment;
    }

    public Task<Appointment?> GetAppointment(int id)
    {
        return _dbContext.Appointments.FirstOrDefaultAsync(a => a.Id == id);
    }

    public Task<List<Appointment>> GetAllAppointments()
    {
        return _dbContext.Appointments.ToListAsync();
    }

    public Task<List<Appointment>> GetAppointmentsByCriteria(DateTime? date, int? doctorId, int? patientId)
    {
        IQueryable<Appointment> query = _dbContext.Appointments;

        if (date.HasValue)
        {
            query = query.Where(a => a.Date.Date == date.Value.Date);
        }

        if (doctorId.HasValue)
        {
            query = query.Where(a => a.Doctor.Id == doctorId.Value);
        }

        if (patientId.HasValue)
        {
            query = query.Where(a => a.Patient.Id == patientId.Value);
        }

        return query.ToListAsync();
    }

}