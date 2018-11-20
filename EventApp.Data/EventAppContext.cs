using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

namespace EventApp.Data
{
    public class EventAppContext : DbContext
    {
        public DbSet<Location> Locations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<StaffRole> StaffRoles { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<EventGuest> EventGuests { get; set; }

        public static readonly LoggerFactory MyLoggerFactory
             = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=GHOST;Initial Catalog=NLayerSample;User id=internship")
            //.UseLoggerFactory(MyLoggerFactory)
            .UseLazyLoadingProxies(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventGuest>()
                .HasKey(a => new { a.EventId, a.GuestId });


        }
    }
}
