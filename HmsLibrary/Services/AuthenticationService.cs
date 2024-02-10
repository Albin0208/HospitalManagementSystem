﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsAPI.Data;
using HmsAPI.DTO.RequestDTO;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;
using HmsLibrary.Services.EmployeeServices;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace HmsLibrary.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HmsDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IPatientService _patientService;
    private readonly IEmployeeService _employeeService;

    public AuthenticationService(HmsDbContext dbContext, UserManager<ApplicationUser> userManager, IPatientService patientService, IEmployeeService employeeService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _patientService = patientService;
        _employeeService = employeeService;
    }

    public bool SignIn(string username, string password)
    {
        // Grab the user with the username
        var user = _dbContext.Employees.FirstOrDefault(u => u.Username == username);
        // Check if the user exists
        if (user == null)
        {
            return false;
        }

        // Verify the password
        return Util.PasswordHasher.VerifyPassword(password, user.Password);
    }

    public bool SignUp(Employee employee, string password)
    {
        // Check if the user already exists
        if (_dbContext.Employees.Any(u => u.Username == employee.Username))
        {
            // Employee already exists
            return false;
        }

        // Encrypt password
        password = Util.PasswordHasher.HashPassword(password);

        employee.Password = password;
        _dbContext.Employees.Add(employee);
        try
        {
            _dbContext.SaveChanges();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving user: {e.Message}");
            return false;
        }
    }

    public async Task<IdentityResult> RegisterEmployee(RegisterRequest request)
    {
        var result = await RegisterBaseUser(request);

        if (result.Result.Succeeded)
        {
            // TODO Add employee specific data to the employee table
        }

        return result.Result;
    }

    public async Task<IdentityResult> RegisterPatient(PatientRegisterRequest request)
    {
        var result = await RegisterBaseUser(new RegisterRequest
        {
            Email = request.Email,
            Password = request.Password
        });

        if (!result.Result.Succeeded) return result.Result;

        // TODO Add patient specific data to the patient table
        var user = result.User;

        var patient = new Patient
        {
            Id = user.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            // TODO Handle the rest of the properties
            //DateOfBirth = request.DateOfBirth,
            //PhoneNumber = request.PhoneNumber,
            //Address = request.Address,
            //City = request.City,
            //ZipCode = request.ZipCode,
            //Country = request.Country,
        };

        try
        {
            var res = await _patientService.CreatePatient(patient);

            return IdentityResult.Success;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating patient: {e.Message}");
            return IdentityResult.Failed();
        }
    }

    private async Task<RegisterResult> RegisterBaseUser(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        return new RegisterResult()
        {
            Result = result,
            User = user
        };
    }
}
public class RegisterResult
{
    public IdentityResult Result { get; set; }
    public ApplicationUser User { get; set; }
}
