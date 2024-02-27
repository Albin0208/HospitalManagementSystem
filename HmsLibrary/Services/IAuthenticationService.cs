﻿using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace HmsLibrary.Services;

public interface IAuthenticationService
{
    Task<IdentityResult> RegisterPatient(PatientRegisterRequest request);
    Task<IdentityResult> RegisterEmployee(PatientRegisterRequest request);
    Task<string> SignIn(string username, string password);
}