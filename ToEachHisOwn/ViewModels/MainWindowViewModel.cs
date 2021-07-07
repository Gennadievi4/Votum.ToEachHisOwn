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
        private string _SelectedComboBoxString;

        private IDictionary<string, string[]> _Data;
        private readonly IJsonDataServices _JsonServices;
        private readonly IDialogServices _Dialog;
        #endregion

        #region Properties
        public string Title => "Каждому своё!";
        public string SelectedComboBoxString
        {
            get => _SelectedComboBoxString;
            set => Set(ref _SelectedComboBoxString, value);
        }
        public IDictionary<string, string[]> Data
        {
            get => _Data;
            set => Set(ref _Data, value);
        }
        #endregion

        #region Constructor
        public MainWindowViewModel(IJsonDataServices data, IDialogServices dialog)
        {
            Data = data.GetDbFromExe();
            _JsonServices = data;
            _Dialog = dialog;
        }
        #endregion

        #region Commands
        public ICommand ClossApp => new CloseWindowCommand();
        public ICommand OpenFile => new LambdaCommand<object>(obj =>
        {
            if (_Dialog.OpenFileDialog() == true)
            {
                Data = _JsonServices.Open(_Dialog.FilePath, _Dialog.FileNameWithoutExstension);
                _Dialog.ShowMessage($"Импорт базы \"{_Dialog.FileNameWithoutExstension}\" завершён.");
                SelectedComboBoxString = _JsonServices.MatchOfNameBase;
            }
        });

        public ICommand DeleteBaseFromApp => new LambdaCommand<string>(str =>
        {
            _Dialog.ShowMessage($"Выбранная база \"{SelectedComboBoxString}\" удалена.");
            Data = _JsonServices.Delete(str);
        },
            str =>
            {
                return !string.IsNullOrEmpty(SelectedComboBoxString);
            });
        #endregion
    }
}
