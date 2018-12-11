using EventApp.Data.Infrastructure;
using EventApp.Services.Events;
using EventApp.Services.Guests;
using EventApp.Services.Locations;
using EventApp.Services.Staffs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Services.Infrastructure
{
    public static class ServicesDependencyMapper
    {
        public static IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration)
        {

            services = DataDependencyMapper.GetDependencies(services, configuration);

            services.AddScoped<IEventService, EventService>();
            services.AddScoped<IGuestService, GuestService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IStaffService, StaffService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
