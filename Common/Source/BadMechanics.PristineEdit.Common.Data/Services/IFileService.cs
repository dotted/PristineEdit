namespace BadMechanics.PristineEdit.Common.Data.Services
{
    using System.IO;

    public interface IFileService
    {
        Stream OpenFile(string defaultExtension, string filter);
        Stream SaveFile(string defaultExtension, string filter);       
    }
}
