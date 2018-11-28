using EventApp.Data.Enums;
using EventApp.DTOs;
using EventApp.Services.Events;
using EventApp.Services.Infrastructure;
using EventApp.Services.Locations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
                foreach (var item in eventService.GetEventsBySize(EventSize.XL))
                    System.Console.WriteLine(item.Name);


                var a = new EventDTO
                {
                    Description = "test bogdan",
                    Name = "test bogdan",
                    StartTime = System.DateTime.Today,
                    LocationId=1,
                    EstimatedBudget=10000,
                    EventTypeId=1
                };
                a.EndTime = a.StartTime.AddDays(1);

                var b = new LocationDTO
                {
                    Name = "test bogdan",
                    Address="test bogdan",
                    Capacity=-2,
                    RentFee=1
                   
                };
                //eventService.CreateEvent(a);
                eventService.ChangeEventLocation(1081, b);

                foreach (var item in locationService.GetLocations())
                {
                    System.Console.WriteLine(item.Name);
                }
            }
        }
    }
}
