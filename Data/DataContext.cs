using GardenApp.API.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GardenApp.API.Data
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DataContext(DbContextOptions<DataContext> options)
        : base(options)
        {
        }
    }
}