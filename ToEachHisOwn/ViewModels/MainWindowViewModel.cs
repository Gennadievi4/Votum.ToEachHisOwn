using System.Windows.Input;
using ToEachHisOwn.Infrastructure.Commands;
using ToEachHisOwn.ViewModels.Base;

namespace ToEachHisOwn.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Fields
        private string _Title;
        public string Title => _Title = "Каждому своё!";
        #endregion

        #region Commands
        public ICommand ClossApp => new CloseWindowCommand();
        #endregion
    }
}
