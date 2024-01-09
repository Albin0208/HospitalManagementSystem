﻿using System;
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

    public string GetPatientName()
    {
        return "John Doe";
    }

    public Task<Patient?> GetPatient(int id)
    {
        return _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
    }
    /// <inheritdoc />
    public async Task<int> CreatePatient(Patient patient)
    {
        await _dbContext.Patients.AddAsync(patient);
        return await _dbContext.SaveChangesAsync();
    }
}