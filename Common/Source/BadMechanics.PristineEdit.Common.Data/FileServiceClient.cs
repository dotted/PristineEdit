namespace BadMechanics.PristineEdit.Common.Data
{
    using System.IO;

    using BadMechanics.PristineEdit.Common.Interfaces.Services;

    public class FileServiceClient
    {
        private readonly IFileService fileService;

        public FileServiceClient(IFileService fileService)
        {
            this.fileService = fileService;
        }

        public Stream OpenFile(string defaultExtension, string filter)
        {
            return this.fileService.OpenFile(defaultExtension, filter);
        }

        public Stream SaveFile(string defaultExtension, string filter)
        {
            return this.fileService.SaveFile(defaultExtension, filter);
        }
    }
}
