using ChefsRegistry.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Win32;
using System;

namespace ChefsRegistry.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ChefModel> tbl_Chefs { get; set; }
        public DbSet<AddressModel> Address { get; set; }
        public DbSet<PhoneModel> Phone { get; set; }
        public DbSet<RestaurantModel> Restaurant { get; set; }
        public DbSet<LogEventsModel> LogEvents { get; set; }
        public DbSet<RegistryViewModel> Chef { get; set; }

        public DbSet<RegistryChefModel> RegistryChefModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RegistryChefModel>().HasNoKey();
        }



    }
}
