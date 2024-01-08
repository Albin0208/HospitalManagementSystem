using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;

namespace HmsApp;

public class MainWindowViewModel
{
    private readonly IPatientService _patientService;
    public string? Name { get; set; }

    public RelayCommand CreateUserCommand { get; set; }

    public MainWindowViewModel(IPatientService patientService)
    {
        _patientService = patientService;

        CreateUserCommand = new RelayCommand(CreateUser, _ => true);
    }

    private async Task CreateUser()
    {
        //Name = _patientService.GetPatientName();
        var p = await _patientService.GetPatient(2);
        _patientService.CreatePatient(Name);
    }
}
