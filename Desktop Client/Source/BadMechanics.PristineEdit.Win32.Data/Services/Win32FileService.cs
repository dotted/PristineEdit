namespace BadMechanics.PristineEdit.Win32.Data.Services
{
    using System;
    using System.IO;
    using BadMechanics.PristineEdit.Data.Services;
    using Microsoft.Win32;

    public class Win32FileService : IFileService
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="defaultExtension"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Th
        /// </summary>
        /// <param name="defaultExtension"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
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
