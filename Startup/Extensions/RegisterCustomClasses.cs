using Microsoft.Extensions.DependencyInjection;
using ShortLinks.Services;

namespace ShortLinks.Startup.Extensions
{
    public static class RegisterCustomClassesExtension
    {
        public static void RegisterCustomClasses(this IServiceCollection services)
        {
            // Services
            services.AddTransient<IShortLinksService, ShortLinksService>();
        }

    }
}
