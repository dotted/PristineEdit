namespace BadMechanics.PristineEdit.Common.Data
{
    using System.IO;

    using BadMechanics.PristineEdit.Common.Interfaces.Services;

    using global::Common.Logging;

    public class FileServiceClient
    {
        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILog Log;

        /// <summary>
        /// The file service
        /// </summary>
        private readonly IFileService fileService;

        static FileServiceClient()
        {
            Log = LogManager.GetCurrentClassLogger();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileService">The file service to use</param>
        public FileServiceClient(IFileService fileService)
        {
            this.fileService = fileService;
        }

        /// <summary>
        /// Open file using file service to call platform specific file dialog
        /// </summary>
        /// <param name="defaultExtension">Te default extension of files to open</param>
        /// <param name="filter">The filter</param>
        /// <returns></returns>
        public Stream OpenFile(string defaultExtension, string filter)
        {
            Log.Debug(m => m("OpenFile defaultExtension: {0}, filter: {1}", defaultExtension, filter));
            return this.fileService.OpenFile(defaultExtension, filter);
        }

        /// <summary>
        /// Save file using file service to call platform specific file dialog
        /// </summary>
        /// <param name="defaultExtension">Te default extension of files to save</param>
        /// <param name="filter">The filter</param>
        /// <returns></returns>
        public Stream SaveFile(string defaultExtension, string filter)
        {
            Log.Debug(m => m("SaveFile defaultExtension: {0}, filter: {1}", defaultExtension, filter));
            return this.fileService.SaveFile(defaultExtension, filter);
        }
    }
}
