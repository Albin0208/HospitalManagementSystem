using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using HmsLibrary.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HmsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");
            var dbContext = new HmsDbContext(new DbContextOptions<HmsDbContext>(), connectionString);

            base.OnStartup(e); // Call the base method
        }
    }

}
