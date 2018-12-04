using EventApp.Data.Enums;
using EventApp.DTOs;
using EventApp.Services.Events;
using EventApp.Services.Infrastructure;
using EventApp.Services.Locations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;

namespace EventApp.Services
{
    public class Program
    {
        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

        static void Main(string[] args)
        {
            var services = DependencyMapper.GetDependencies(GetConfiguration()).BuildServiceProvider();

            using (var scope = services.CreateScope())
            {
                var eventService = scope.ServiceProvider.GetService<IEventService>();
                var locationService = scope.ServiceProvider.GetService<ILocationService>();

                //locationService.DeleteLocationById(1110);
                foreach (var item in eventService.GetEvents())
                {
                    System.Console.WriteLine(item.Name);
                }

                //foreach (var item in locationService.GetLocations())
                //{
                //    System.Console.WriteLine(item.Name);
                //}
            }
        }
    }
}
