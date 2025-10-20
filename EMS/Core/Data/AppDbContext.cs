using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace EMS.Core.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext() { }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SoilData> SoilSamples { get; set; }
        public DbSet<WaterData> WaterSamples { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<SpeciesData> SpeciesData { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserTask> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=EMSApp.db");
            }
            
        }

    }
}
