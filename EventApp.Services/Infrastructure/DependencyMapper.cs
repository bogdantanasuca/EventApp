﻿using EventApp.Data;
using EventApp.Data.Infrastructure;
using EventApp.Services.Events;
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

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
