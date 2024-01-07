using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Services;

namespace HmsApp;

public class MainWindowViewModel
{
    public string  Text { get; set; }
    private readonly IPatientService _patientService;

    public MainWindowViewModel(IPatientService patientService)
    {
        _patientService = patientService;
        Text = _patientService.GetPatientName();
    }
}
