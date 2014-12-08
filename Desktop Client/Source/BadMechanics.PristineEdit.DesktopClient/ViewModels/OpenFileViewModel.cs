using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BadMechanics.PristineEdit.DesktopClient.ViewModels
{
    class OpenFileViewModel
    {
        private ICommand openCommand;

        public ICommand OpenCommand
        {
            get
            {
                if (openCommand == null)
                {
                    openCommand = new DelegateCommand(OpenTextFile);
                }
                return openCommand;
            }
        }

        public void OpenTextFile(object document)
        {

        }
    }
}
