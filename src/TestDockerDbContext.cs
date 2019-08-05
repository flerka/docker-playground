using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace TestDocker
{
    public class TestDockerDbContext : DbContext
    {
        public DbSet<AppLaunchHistoryEntry> AppLaunchHistoryEntries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppLaunchHistoryEntry>().HasKey(i => i.Id);
        }

        public class AppLaunchHistoryEntry
        {
            public int Id { get; set; }
            public DateTime AppLaunchTime { get; set; }
        }
    }

}
