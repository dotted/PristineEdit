namespace BadMechanics.PristineEdit.Win32.DesktopClient.ViewModels
{
    using System;
    using System.IO;
    using System.Windows.Input;

    using BadMechanics.PristineEdit.Common.Data;
    using BadMechanics.PristineEdit.Win32.Data.Services;

    using global::Common.Logging;

    class FileDialogViewModel : ViewModel
    {
        private static readonly ILog Log;

        static FileDialogViewModel()
        {
            Log = LogManager.GetCurrentClassLogger();
        }
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
            var fileService = new FileServiceClient(new Win32FileService());
            this.Stream = fileService.SaveFile(this.Extension, this.Filter);
        }
        private void OpenFile()
        {
            var fileService = new FileServiceClient(new Win32FileService());
            this.Stream = fileService.OpenFile(this.Extension, this.Filter);        
        }
    }
}
