namespace BadMechanics.PristineEdit.DesktopClient.Services.Win32
{
    using System;
    using System.IO;

    using Microsoft.Win32;

    class Win32FileService : IFileService
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
