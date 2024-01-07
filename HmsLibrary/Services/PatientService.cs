using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;

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

    public Patient GetPatient(int id)
    {
        return _dbContext.Patients.FirstOrDefault(p => p.Id == id);
    }
}