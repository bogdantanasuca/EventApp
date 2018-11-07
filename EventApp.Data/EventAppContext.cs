using System;
using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventApp.Data
{
    public class EventAppContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=GHOST;Initial Catalog=NLayerSample;User id=internship");
        }
    }
}
