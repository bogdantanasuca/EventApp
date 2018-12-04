using EventApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace EventApp.Data
{
    public class EventAppDataContext : DbContext
    {
        private static readonly LoggerFactory LoggerFactory = new LoggerFactory(new[] { new ConsoleLoggerProvider((_, __) => true, true) });

        public virtual DbSet<Staff> Staffs { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Guest> Guests { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<StaffRole> StaffRoles { get; set; }
        public virtual DbSet<EventType> EventTypes { get; set; }
        public virtual DbSet<EventGuest> EventGuests { get; set; }

        public EventAppDataContext(DbContextOptions<EventAppDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventGuest>().HasKey(e => new { e.EventId, e.GuestId });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}
