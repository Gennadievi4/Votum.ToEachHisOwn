using Microsoft.Extensions.DependencyInjection;

namespace ToEachHisOwn.ViewModels
{
    internal class ViewModelLocator
    {
        public MainWindowViewModel MainWindowModel => App.Host.Services.GetRequiredService<MainWindowViewModel>();
    }
}
