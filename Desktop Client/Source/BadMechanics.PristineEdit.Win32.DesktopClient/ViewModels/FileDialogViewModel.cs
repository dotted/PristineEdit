namespace BadMechanics.PristineEdit.Win32.DesktopClient.ViewModels
{
    using System;
    using System.IO;
    using System.Windows.Input;
    using BadMechanics.PristineEdit.Data.Services;
    using BadMechanics.PristineEdit.Win32.Data.Services;

    class FileDialogViewModel : ViewModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FileDialogViewModel()
        {
            this.SaveCommand = new RelayCommand(this.SaveFile);
            this.OpenCommand = new RelayCommand(this.OpenFile);
        }

        /// <summary>
        /// Properties declaration
        /// </summary>
        public ICommand OpenCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        /// <summary>
        /// Stream: Used to get the data representing the SOAP request or SOAP response in the form of a Stream.
        /// </summary>
        public Stream Stream { get; set; }

        public String Extension { get; set; }
        public String Filter { get; set; }

        /// <summary>
        /// Use factory method patter to allow unit testing and cross platform extenstion
        /// </summary>
        private void SaveFile()
        {
            var fileService = new FileServiceClient(new Win32FileService());
            this.Stream = fileService.SaveFile(this.Extension, this.Filter);
        }
        /// <summary>
        /// Use factory method patter to allow unit testing and cross platform extenstion
        /// </summary>
        private void OpenFile()
        {
            var fileService = new FileServiceClient(new Win32FileService());
            this.Stream = fileService.OpenFile(this.Extension, this.Filter);        
        }
    }
}
