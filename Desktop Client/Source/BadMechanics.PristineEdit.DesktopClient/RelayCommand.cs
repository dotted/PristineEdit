using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BadMechanics.PristineEdit.DesktopClient
{
    public sealed class RelayCommand : ICommand
    {
        private Action function;

        public RelayCommand(Action function)
        {
            this.function = function;
        }

        public bool CanExecute(object parameter)
        {
            return this.function != null;
        }

        public void Execute(object parameter)
        {
            if (this.function != null)
            {
                this.function();
            }
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
    
}
