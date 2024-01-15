using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HmsLibrary.Data.Model;
using HmsLibrary.Data.Model.Employees;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Data.Context;

public class HmsDbContext : DbContext
{
    public HmsDbContext(DbContextOptions<HmsDbContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; }
    public DbSet<Employee> Employees { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Get all entities that inherit from BaseEntity
        var entityTypes = Assembly.GetAssembly(typeof(BaseEntity))?.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(BaseEntity)));

        if (entityTypes == null) return;

        foreach (var entity in entityTypes)
        {
            modelBuilder.Entity(entity).Property<DateTime>("CreatedAt").HasDefaultValueSql("getdate()");
            modelBuilder.Entity(entity).Property<DateTime>("UpdatedAt").ValueGeneratedOnAddOrUpdate();
        }

        modelBuilder.Entity<Employee>().HasDiscriminator<string>("Role")
            .HasValue<Employee>("Employee")
            .HasValue<Doctor>("Doctor"); // Add more roles here

        base.OnModelCreating(modelBuilder);
    }
}
