using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Services;

public class PatientService : IPatientService
{
    private readonly HmsDbContext _dbContext;

    public PatientService(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc />
    public Task<Patient?> GetPatient(Guid id)
    {
        return _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <inheritdoc />
    public async Task<Patient> CreatePatient(Patient patient)
    {
        ArgumentNullException.ThrowIfNull(patient);

        if (string.IsNullOrWhiteSpace(patient.FirstName))
        {
            throw new ArgumentException("Firstname cannot be empty or null.", nameof(patient.FirstName));
        }

        if (string.IsNullOrWhiteSpace(patient.LastName))
        {
            throw new ArgumentException("Lastname cannot be empty or null.", nameof(patient.LastName));
        }

        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();

        return patient;
    }

    /// <inheritdoc />
    public Task<List<Patient>> GetPatients()
    {
        return _dbContext.Patients.ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Patient> UpdatePatient(Patient patient)
    {
        ArgumentNullException.ThrowIfNull(patient);

        var dbPatient = await GetPatient(patient.Id);

        if (dbPatient == null)
        {
            throw new ArgumentException("Patient does not exist.", nameof(patient));
        }

        // Update the patient with the new values
        _dbContext.Entry(dbPatient).CurrentValues.SetValues(patient);
        await _dbContext.SaveChangesAsync();

        return patient;
    }

    public async Task<Patient> DeletePatient(Guid id)
    {
        var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id) ?? throw new ArgumentException("Patient does not exist.", nameof(id));

        _dbContext.Patients.Remove(patient);
        await _dbContext.SaveChangesAsync();

        return patient;
    }
}