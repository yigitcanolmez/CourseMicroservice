using FreeCourse.Services.Catalog.Services.CategoryServices;
using FreeCourse.Services.Catalog.Services.CourseServices;
using FreeCourse.Services.Catalog.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace FreeCourse.Services.Catalog.DependencyContainer
{
    public static class ConfigureContainerExtensions
    {
        public static IServiceCollection AddContainerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.Authority = configuration["IdentityServerURL"];
                opt.Audience = "resource_catalog";
                opt.RequireHttpsMetadata = false;
            });
            #region Database Settings

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

            services.AddSingleton<IDatabaseSettings>(sp =>
            {
                return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
            });
            #endregion

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICourseService, CourseService>();


            return services;
        }

    }
}
