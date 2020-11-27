using Microsoft.Extensions.DependencyInjection;

namespace ToEachHisOwn.Services
{
    internal static class ServiceRegistration
    {
        internal static IServiceCollection RegisterService(this IServiceCollection services) 
        {
            return services;
        }
    }
}
