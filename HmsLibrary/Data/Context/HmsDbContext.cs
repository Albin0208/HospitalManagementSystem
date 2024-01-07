using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HmsLibrary.Data.Context;

public class HmsDbContext : DbContext
{
    private readonly string _connectionString;

    public HmsDbContext(DbContextOptions<HmsDbContext> options, string connectionString) : base(options)
    {
        _connectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

}
