using EventApp.Data;
using EventApp.Data.Infrastructure;
using EventApp.Services.Events;
using EventApp.Services.Guests;
using EventApp.Services.Locations;
using EventApp.Services.Staffs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Services.Infrastructure
{
    public static class DependencyMapper
    {
        public static ServiceCollection GetDependencies(IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("EventAppConnectionString");

            var services = new ServiceCollection();
            services.AddDbContext<EventAppDataContext>(options => options.UseSqlServer(connection));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IStaffService, StaffService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
