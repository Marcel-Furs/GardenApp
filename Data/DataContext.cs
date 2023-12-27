using GardenApp.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GardenApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Calendar> Calendars { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<History> Histories { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantProfile> PlantProfiles { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<WeatherMeasurement> WeatherMeasurements { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
    }
}