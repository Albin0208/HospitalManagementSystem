using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.DTO;
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

    public async Task<Appointment> CreateAppointment(AppointmentDTO appointment)
    {
        // Check if the doctor exists
        var doctor = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == appointment.DoctorId) ?? throw new ArgumentException("Doctor does not exist or is not specified", nameof(appointment));

        // Check if the patient exists
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == appointment.PatientId) ?? throw new ArgumentException("Patient does not exist or is not specified", nameof(appointment));

        var newAppointment = new Appointment
        {
            Date = appointment.Date,
            Reason = appointment.Reason,
            Notes = appointment.Notes,
            Doctor = doctor,
            Patient = patient
        };

        await _dbContext.Appointments.AddAsync(newAppointment);
        await _dbContext.SaveChangesAsync();

        return newAppointment;
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

    public async Task<Appointment> DeleteAppointment(int id)
    {
        var appointment = await _dbContext.Appointments.FindAsync(id) ?? throw new ArgumentException($"Appointment with ID {id} not found.", nameof(id));
        _dbContext.Appointments.Remove(appointment);
        await _dbContext.SaveChangesAsync();

        return appointment;
    }
}