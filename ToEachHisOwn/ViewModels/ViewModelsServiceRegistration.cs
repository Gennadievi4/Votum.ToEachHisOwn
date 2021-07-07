using Microsoft.Extensions.DependencyInjection;

namespace ToEachHisOwn.ViewModels
{
    internal static class ViewModelsServiceRegistration
    {
        internal static IServiceCollection ViewModelsRegistrator(this IServiceCollection services)
        {
            services.AddSingleton<MainWindowViewModel>();
            return services;
        }
    }
}
