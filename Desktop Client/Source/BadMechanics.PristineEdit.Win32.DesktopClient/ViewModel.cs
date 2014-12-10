using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMechanics.PristineEdit.Win32.DesktopClient
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Event used to handle property changes
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// This method is called whenever the property is changed.
        /// </summary>
        /// <param name="propertyName">
        /// Name of property changed
        /// </param>
        public void RaisePropertyChanged(string propertyName)
        {
            // Sets the PropertyChangedEventHandler varible to whatever property was changed.
            PropertyChangedEventHandler handler = this.PropertyChanged;

            // If "handler" is not empty, call PropertyChangedEventArgs
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
