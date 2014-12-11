namespace BadMechanics.PristineEdit.Win32.DesktopClient.ViewModels
{
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Windows.Input;

    public class MainViewModel : ViewModel
    {
        /// <summary>
        /// Main constructor that creates a new instance of save, open and new command.
        /// </summary>
        public MainViewModel()
        {
            this.SaveCommand = new RelayCommand(this.SaveFile);
            this.OpenCommand = new RelayCommand(this.OpenFile);
            this.NewCommand = new RelayCommand(this.NewFile);
        }

        /// <summary>
        /// Properties declaration
        /// </summary>
        public ICommand OpenCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand NewCommand { get; set; }
        private Encoding encoding;
        private string text;

        /// <summary>
        /// Gets and sets the encoding used to handle strings
       /// </summary>
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

        
        /// <summary>
        /// Reads the text to and from the textbox or sets the text equal to the encoded value and calls the RaisedPropertyChanged method.
        /// </summary>
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

        /// <summary>
        /// Async task that opens the "open file" dialog and loads .txt files into the textfield
        /// </summary>
        private async void OpenFile()
        {
            var fileDialog = new FileDialogViewModel();
            fileDialog.Extension = "*.txt";
            fileDialog.Filter = "Text documents (.txt)|*.txt";
            fileDialog.OpenCommand.Execute(null);

            using (var sr = new StreamReader(fileDialog.Stream, true))
            {
                this.Encoding = sr.CurrentEncoding;
                this.Text = await sr.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Async task that calls FileDialogViewModel, takes the content of the textbox and excecutes the SaveCommand
        /// </summary>
        private async void SaveFile()
        {
            var fileDialog = new FileDialogViewModel();
            fileDialog.Extension = "*.txt";
            fileDialog.Filter = "Text documents (.txt)|*.txt";
            fileDialog.SaveCommand.Execute(null);

            using (var sr = new StreamWriter(fileDialog.Stream, this.Encoding))
            {
                await sr.WriteAsync(this.Text.ToString(CultureInfo.InvariantCulture));
            }       
        }

        /// <summary>
        /// Task used to clear the textbox
        /// </summary>
        private void NewFile()
        {
            this.Text = string.Empty;
        }
    }
}
