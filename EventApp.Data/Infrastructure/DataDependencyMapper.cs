using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventApp.Data.Infrastructure
{
    public class DataDependencyMapper
    {
        public static IServiceCollection GetDependencies(IServiceCollection services, IConfiguration configuration)
        {

            var connection = configuration.GetConnectionString("EventAppConnectionString");
            services.AddDbContext<EventAppDataContext>(options => options.UseSqlServer(connection));
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
