using Microsoft.Win32;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using ToEachHisOwn.Services.Interfaces;

namespace ToEachHisOwn.Services
{
    class WindowsUserDialogsService : IDialogServices
    {
        public string FilePath { get; set; }
        public string FileNameWithoutExstension { get; set; }

        public bool OpenFileDialog()
        {
            OpenFileDialog OpenFile = new OpenFileDialog
            {
                DefaultExt = "*.txt",
                Title = "Открыть базу",
                Filter = "Тектовые документы: (*.json; *.txt)|*.json; *.txt",
                InitialDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                ShowReadOnly = true,
                ValidateNames = true,
                CheckFileExists = true,
                RestoreDirectory = true,
                Multiselect = false,
                ReadOnlyChecked = true,
            };

            if (OpenFile.ShowDialog() == true)
            {
                FilePath = OpenFile.FileName;
                FileNameWithoutExstension = string.Concat(OpenFile.SafeFileName.TakeWhile(x => x != '.'));
                return true;
            }

            return false;
        }

        public bool SaveFileDialog()
        {
            SaveFileDialog SaveFile = new SaveFileDialog();
            if (SaveFile.ShowDialog() == true)
            {
                FilePath = SaveFile.FileName;
                return true;
            }

            return false;
        }

        public void ShowMessage(string message)
        {
            MessageBox.Show(message, "Информация", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
