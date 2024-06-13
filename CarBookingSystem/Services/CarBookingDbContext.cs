using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarBookingSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace CarBookingSystem.Services
{
    public class CarBookingDbContext : DbContext
    {
        public DbSet<Car> Cars { get; init; }      

        public DbSet<Booking> Bookings { get; init; }

        public CarBookingDbContext(DbContextOptions options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Car>();
            modelBuilder.Entity<Booking>();
        }
    }
}