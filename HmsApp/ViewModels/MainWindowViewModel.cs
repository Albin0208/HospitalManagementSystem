using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services;

namespace HmsApp.ViewModels;

public class MainWindowViewModel
{
    private readonly IPatientService _patientService;
    private readonly IAuthenticationService _authenticationService;

    public string? Username { get; set; }
    public string Password { get; set; }

    public RelayCommand SignInCommand { get; set; }

    public MainWindowViewModel(IPatientService patientService, IAuthenticationService authenticationService)
    {
        _patientService = patientService;
        _authenticationService = authenticationService;

        SignInCommand = new RelayCommand(SignInUser, _ => true);
    }

    private async Task SignInUser()
    {
        // Do null check on password and username

        //var result = _authenticationService.SignIn(Username, Password);

    }
}
