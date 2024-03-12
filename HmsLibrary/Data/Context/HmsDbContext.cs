using System;
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
    //public DbSet<EmployeeRole> EmployeeRoles { get; set; }
    
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

    //    modelBuilder.Entity<Employee>()
    //.HasOne(e => e.Role)
    //.WithMany(r => r.Employees)
    //.HasForeignKey(e => e.RoleId)
    //.IsRequired();

        var roleManager = modelBuilder.Entity<IdentityRole<Guid>>();

        var adminRoleId = Guid.NewGuid();

        roleManager.HasData(
            new IdentityRole<Guid> { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Patient", NormalizedName = "PATIENT" },
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Employee", NormalizedName = "EMPLOYEE" },
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Doctor", NormalizedName = "DOCTOR" },
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Nurse", NormalizedName = "NURSE" },
            new IdentityRole<Guid> { Id = Guid.NewGuid(), Name = "Receptionist", NormalizedName = "RECEPTIONIST" }
        );
        
        // Create a new master user
        var hasher = new PasswordHasher<ApplicationUser>();

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = "admin",
            NormalizedUserName = "ADMIN",
            SecurityStamp = Guid.NewGuid().ToString()
        };

        user.PasswordHash = hasher.HashPassword(user, "admin");

        // Add the admin to the admin role
        modelBuilder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
        {
            RoleId = adminRoleId,
            UserId = user.Id
        });

        modelBuilder.Entity<ApplicationUser>().HasData(user);



    }
}
