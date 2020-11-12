using ToEachHisOwn.ViewModels.Base;

namespace ToEachHisOwn.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private string _Title;
        public string Title => _Title = "Каждому своё!";
    }
}
