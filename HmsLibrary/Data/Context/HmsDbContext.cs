﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HmsAPI.Data;
using HmsLibrary.Data.Model;
using HmsLibrary.Data.Model.Employees;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace HmsLibrary.Data.Context;

public class HmsDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public HmsDbContext(DbContextOptions<HmsDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<EmployeeRole> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Get all entities that inherit from BaseEntity
        var entityTypes = Assembly.GetAssembly(typeof(BaseEntity))?.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(BaseEntity)));

        if (entityTypes != null)
            foreach (var entity in entityTypes)
            {
                modelBuilder.Entity(entity).Property<DateTime>("CreatedAt").HasDefaultValueSql("GETDATE()");
                modelBuilder.Entity(entity).Property<DateTime>("UpdatedAt").HasDefaultValueSql("GETDATE()").ValueGeneratedOnAddOrUpdate();
            }

        // Seed default roles
        modelBuilder.Entity<EmployeeRole>().HasData(
            new EmployeeRole { Id = Guid.NewGuid(), RoleName = "Admin" },
            new EmployeeRole { Id = Guid.NewGuid(), RoleName = "Doctor" },
            new EmployeeRole { Id = Guid.NewGuid(), RoleName = "Nurse" },
            new EmployeeRole { Id = Guid.NewGuid(), RoleName = "Receptionist" }
        );

        modelBuilder.Entity<Employee>()
            .HasOne(e => e.Role)
            .WithMany(r => r.Employees)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();
    }
}
