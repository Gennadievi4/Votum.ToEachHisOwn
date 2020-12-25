using System.Collections.Generic;
using System.Windows.Input;
using ToEachHisOwn.Infrastructure.Commands;
using ToEachHisOwn.Services.Interfaces;
using ToEachHisOwn.ViewModels.Base;

namespace ToEachHisOwn.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Fields
        public string Title => "Каждому своё!";
        #endregion

        #region Properties
        public ICommand ClossApp => new CloseWindowCommand();

        public IDictionary<string, string[]> Data { get; }
        #endregion

        #region Constructor
        public MainWindowViewModel(IJsonData data)
        {
            Data = data.GetDbFromExe();
        }
        #endregion
    }
}
