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
    public Task<Patient?> GetPatient(int id)
    {
        return _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }

    /// <inheritdoc />
    public async Task<Patient> CreatePatient(Patient patient)
    {
        await _dbContext.Patients.AddAsync(patient);
        await _dbContext.SaveChangesAsync();

        return patient;
    }

    /// <inheritdoc />
    public Task<List<Patient>> GetPatients()
    {
        return _dbContext.Patients.ToListAsync();
    }
}