using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;

namespace BadMechanics.PristineEdit.DesktopClient.Services.Win32
{
    class Win32FileService : IFileService
    {
        public Stream OpenFile(string defaultExtension, string filter)
        {
            var dialog = new OpenFileDialog();
            dialog.DefaultExt = defaultExtension;
            dialog.Filter = filter;

            var results = dialog.ShowDialog();
            if (results.Value == null)
            {
                throw new NotImplementedException();
            }
            return dialog.OpenFile();

        }

        public Stream SaveFile(string defaultExtension, string filter)
        {
            var dialog = new SaveFileDialog();
            dialog.DefaultExt = defaultExtension;
            dialog.Filter = filter;

            var results = dialog.ShowDialog();
            if (results.Value == null)
            {
                throw new NotImplementedException();
            }
            return dialog.OpenFile();
        }
    }
}
