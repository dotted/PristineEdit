using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BadMechanics.PristineEdit.DesktopClient.ViewModels
{
    class SaveFileViewModel
    {

        private ICommand saveCommand;

        public ICommand SaveCommand
        {
            get
            {
                if (saveCommand == null)
                {
                    saveCommand = new DelegateCommand(SaveToTextFile);
                }
                return saveCommand;
            }
        }

        public void SaveToTextFile(object document)
        {
        }

    }
}
