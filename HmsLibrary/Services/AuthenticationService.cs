using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Context;
using HmsLibrary.Data.Model;

namespace HmsLibrary.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly HmsDbContext _dbContext;

    public AuthenticationService(HmsDbContext dbContext)
    {
        _dbContext = dbContext;
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
}
