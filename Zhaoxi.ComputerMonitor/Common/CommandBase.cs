using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zhaoxi.ComputerMonitor.Common
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return CanExecuteFunc?.Invoke() == true;
        }

        public void Execute(object parameter)
        {
            ExcuteAction?.Invoke(parameter);
        }

        public Action<object> ExcuteAction { get; set; }
        public Func<bool> CanExecuteFunc { get; set; }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
