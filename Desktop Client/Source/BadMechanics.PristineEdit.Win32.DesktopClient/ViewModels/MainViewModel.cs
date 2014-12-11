namespace BadMechanics.PristineEdit.Win32.DesktopClient.ViewModels
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Input;

    public class MainViewModel : ViewModel
    {
        private Encoding encoding;
        private string text;

        public MainViewModel()
        {
            this.SaveCommand = new RelayCommand(this.SaveFile);
            this.OpenCommand = new RelayCommand(this.OpenFile);
            this.NewCommand = new RelayCommand(this.NewFile);
        }

        public Encoding Encoding 
        {
            get 
            {
                if (this.encoding == null)
                    this.encoding = Encoding.Default;

                return this.encoding;
            }
            set
            {
                this.encoding = value;
                this.RaisePropertyChanged("Encoding");
            }
        }

        public string Text 
        { 
            get
            {
                return this.text;
            } 
            set
            {
                this.text = value; 
                this.RaisePropertyChanged("Text");
            } 
        }
        public ICommand OpenCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }

        private async void OpenFile()
        {
            var fileDialog = new FileDialogViewModel
                                 {
                                     Extension = "*.txt",
                                     Filter = "Text documents (.txt)|*.txt"
                                 };

            fileDialog.OpenCommand.Execute(null);
            using (var sr = new StreamReader(fileDialog.Stream, true))
            {
                this.Encoding = sr.CurrentEncoding;
                this.Text = await sr.ReadToEndAsync();
            }
        }
        private void SaveFile()
        {
            var fileDialog = new FileDialogViewModel
                                 {
                                     Extension = "*.txt", 
                                     Filter = "Text documents (.txt)|*.txt"
                                 };

            fileDialog.SaveCommand.Execute(null);
            SaveFile(fileDialog.Stream);
        }

        private async void SaveFile(Stream file)
        {
            using (var sr = new StreamWriter(file, this.Encoding))
            {
                await sr.WriteAsync(this.Text.ToString(CultureInfo.InvariantCulture));
            }    
        }

        private void NewFile()
        {
            this.Text = string.Empty;
        }
    }
}
