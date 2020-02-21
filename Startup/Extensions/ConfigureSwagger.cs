using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ShortLinks.Startup.Extensions
{
    public static class ConfigureSwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }
    }
}
