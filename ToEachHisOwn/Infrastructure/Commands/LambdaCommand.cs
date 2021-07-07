using System;

namespace ToEachHisOwn.Infrastructure.Commands
{
    internal class LambdaCommand<T> : Command
    {
        private readonly Action<T> _Execute;
        private readonly Func<T, bool> _CanExecute;

        public LambdaCommand(Action<T> Execute, Func<T, bool> CanExecute = null)
        {
            _Execute = Execute ?? throw new ArgumentException(nameof(Execute));
            _CanExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _CanExecute?.Invoke((T)parameter) ?? true;

        public override void Execute(object parameter) => _Execute((T)parameter);
    }
}
