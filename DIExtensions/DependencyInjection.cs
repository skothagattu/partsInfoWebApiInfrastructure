using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PartsInfoWebApi.core.Interfaces;
using PartsInfoWebApi.infrastructure.Repositories;
using PartsInfoWebApi.Infrastructure.Repositories;
using PartsInfoWebApi.Interfaces;
using PartsInfoWebApi;
using PartsInfoWebApi.Core.Interfaces;
using PartsInfoWebApi.core.Interfaces.DesignServices;
using PartsInfoWebApi.infrastructure.Repositories.DesignServices;

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
            services.AddScoped<IDWGnumberRepository, DWGnumberRepository>();
            services.AddScoped<ICabAireDWGNumberRepository, CabAireDWGNumberRepository>();
            services.AddScoped<IEcoLogRepository, EcoLogRepository>();
            services.AddScoped<IEcrLogRepository, EcrLogRepository>();
            services.AddScoped<ICMIDescRepository, CMIDescRepository>();
            services.AddScoped<ICMIVendorRepository, CMIVendorRepository>();
            services.AddScoped<ICMIDescVendorRepository, CMIDescVendorRepository>();
            services.AddScoped<IStdPartIndexRepository, StdPartIndexRepository>();
            services.AddScoped<IPromRepository, PromRepository>();
            services.AddScoped<IPromModelRepository, PromModelRepository>();
            services.AddScoped<IMBRepository, MBRepository>();


            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("PartsInfoWebApi")));

            return services;
        }
    }

}
