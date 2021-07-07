using Microsoft.Extensions.DependencyInjection;
using ToEachHisOwn.Services.Interfaces;

namespace ToEachHisOwn.Services
{
    internal static class ServiceRegistration
    {
        internal static IServiceCollection RegisterService(this IServiceCollection services) 
        {
            services.AddSingleton<IJsonDataServices, InJsonDataServices>();
            services.AddSingleton<IDialogServices, WindowsUserDialogsService>();
            return services;
        }
    }
}
