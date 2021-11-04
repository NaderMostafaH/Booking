using Booking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DataAccess.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlServer("Server=DESKTOP-VNCU5DJ;Database=Booking;Trusted_Connection=True;MultipleActiveResultSets=true");
        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<User>().HasData(
                new User {Id = 1 , Email = "nadermostafa11@gmail.com", Password = "12345678" },
                new User {Id = 2 , Email = "nadermostafa12@gmail.com", Password = "12345678" }
                );

            model.Entity<Trip>().HasData(
                          new Trip
                          {
                              Id = 1,
                               Name = "Journy To Cairo",
                               CityName = "Cairo",
                               ImageUrl = "/images/img1.jpg",
                               Price = 5000,
                               CreationDate = DateTime.Now,
                               Content = "<div><h1>This Is Trip Journy To Cairo Content</h1></div>"
                          },
                          new Trip
                          {
                              Id = 2,
                              Name = "Journy To Luxor",
                              CityName = "Luxor",
                              ImageUrl = "/images/img2.jpg",
                              Price = 10000,
                              CreationDate = DateTime.Now,
                              Content = "<div><h1>This Is Trip Journy To Luxor Content</h1></div>"
                          }
                       
                );
        }
        public DbSet<User> User { get;set;  }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
    }

   
}
