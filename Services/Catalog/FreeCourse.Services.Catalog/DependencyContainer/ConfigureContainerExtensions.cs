using FreeCourse.Services.Catalog.Services;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace FreeCourse.Services.Catalog.DependencyContainer
{
    public static class ConfigureContainerExtensions
    {
        public static IServiceCollection AddContainerServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region Database Settings

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });
            #endregion
            
            services.AddScoped<ICategoryService,CategoryService>();

            return services;
        }

    }
}
