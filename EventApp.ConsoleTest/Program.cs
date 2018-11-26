using EventApp.Services.Events;
using EventApp.Services.Infrastructure;
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
                var events = eventService.GetEventsByName("wed");
                foreach (var item in events)
                    System.Console.WriteLine(item.Name);
            }
        }
    }
}
