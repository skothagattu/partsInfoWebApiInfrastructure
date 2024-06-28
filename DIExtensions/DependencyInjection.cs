using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartsInfoWebApi.core.Interfaces;
using PartsInfoWebApi.infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Interfaces;
using PartsInfoWebApi;
using PartsInfoWebApi.Core.Interfaces;

namespace PartsInfoWebApi.Infrastructure.DIExtensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IThreeLetterCodeRepository, ThreeLetterCodeRepository>();
            services.AddScoped<ISubLogRepository, SubLogRepository>();
            services.AddScoped<ID03numberRepository, D03numberRepository>();
            services.AddScoped<ICabAireDWGNumberRepository, CabAireDWGNumberRepository>();
            services.AddScoped<IEcoLogRepository, EcoLogRepository>();
            services.AddScoped<IEcrLogRepository, EcrLogRepository>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("PartsInfoWebApi")));

            return services;
        }
    }

}
