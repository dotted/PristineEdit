using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BadMechanics.PristineEdit.DesktopClient.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private Encoding encoding;
        private string text;

        public MainViewModel()
        {
            this.SaveCommand = new RelayCommand(this.SaveFile);
            this.OpenCommand = new RelayCommand(this.OpenFile);
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
    }
}
