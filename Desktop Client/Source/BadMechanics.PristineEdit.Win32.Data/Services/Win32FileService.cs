namespace BadMechanics.PristineEdit.Win32.Data.Services
{
    using System;
    using System.IO;
    using System.Runtime.Remoting.Messaging;

    using BadMechanics.PristineEdit.Common.Interfaces.Services;

    using global::Common.Logging;

    using Microsoft.Win32;

    public class Win32FileService : IFileService
    {
        private static readonly ILog Log;
        static Win32FileService()
        {
            Log = LogManager.GetCurrentClassLogger();
        }
        public Stream OpenFile(string defaultExtension, string filter)
        {
            Log.Trace(m => m("Opening open file dialog"));
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
            Log.Trace(m => m("Opening save file dialog"));
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
