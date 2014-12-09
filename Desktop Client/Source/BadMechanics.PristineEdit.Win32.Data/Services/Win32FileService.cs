namespace BadMechanics.PristineEdit.Win32.Data.Services
{
    using System;
    using System.IO;

    using BadMechanics.PristineEdit.Common.Data.Services;

    using Microsoft.Win32;

    public class Win32FileService : IFileService
    {
        public Stream OpenFile(string defaultExtension, string filter)
        {
            var dialog = new OpenFileDialog
                             {
                                 DefaultExt = defaultExtension,
                                 Filter = filter
                             };

            var results = dialog.ShowDialog();
            if (results == null)
            {
                throw new NotImplementedException();
            }
            return dialog.OpenFile();

        }

        public Stream SaveFile(string defaultExtension, string filter)
        {
            var dialog = new SaveFileDialog
                             {
                                 DefaultExt = defaultExtension,
                                 Filter = filter
                             };

            var results = dialog.ShowDialog();
            if (results == null)
            {
                throw new NotImplementedException();
            }
            return dialog.OpenFile();
        }
    }
}
