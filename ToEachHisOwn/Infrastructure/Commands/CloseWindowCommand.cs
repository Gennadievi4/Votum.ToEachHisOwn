using System.Linq;
using System.Windows;

namespace ToEachHisOwn.Infrastructure.Commands
{
    internal class CloseWindowCommand : Command
    {
        public override bool CanExecute(object parameter) => true;

        public override void Execute(object p)
        {
            var window = p as Window;

            if (window is null)
                window = Application.Current.Windows
                    .Cast<Window>()
                    .FirstOrDefault(x => x.IsFocused);

            if (window is null)
                window = Application.Current.Windows
                    .Cast<Window>()
                    .FirstOrDefault(x => x.IsActive);

            window?.Close();
        }
    }
}
