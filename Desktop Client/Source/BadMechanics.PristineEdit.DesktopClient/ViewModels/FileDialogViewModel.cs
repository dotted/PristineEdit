using BadMechanics.PristineEdit.DesktopClient.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BadMechanics.PristineEdit.DesktopClient.ViewModels
{
    class FileDialogViewModel : ViewModel
    {
        public FileDialogViewModel()
        {
            this.SaveCommand = new RelayCommand(this.SaveFile);
            this.OpenCommand = new RelayCommand(this.OpenFile);
        }

        public Stream Stream { get; set; }
        public String Extension { get; set; }
        public String Filter { get; set; }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private void SaveFile()
        {
            var fileService = PlatformFactory.GetFileService();
            this.Stream = fileService.SaveFile(this.Extension, this.Filter);
        }
        private void OpenFile()
        {
            var fileService = PlatformFactory.GetFileService();
            this.Stream = fileService.OpenFile(this.Extension, this.Filter);        
        }
    }
}
