using Microsoft.Extensions.DependencyInjection;
using TestConsole.Interfaces;
using TestConsole.Services;

namespace TestConsole.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpClient<IHttpService, HttpService>();
            services.AddSingleton<IFileService, FileService>();
        }
    }
}
