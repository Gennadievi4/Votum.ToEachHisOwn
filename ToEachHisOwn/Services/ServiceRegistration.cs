using Microsoft.Extensions.DependencyInjection;
using ToEachHisOwn.Services.Interfaces;

namespace ToEachHisOwn.Services
{
    internal static class ServiceRegistration
    {
        internal static IServiceCollection RegisterService(this IServiceCollection services) 
        {
            services.AddSingleton<IJsonData, InJsonDataServices>();
            return services;
        }
    }
}
