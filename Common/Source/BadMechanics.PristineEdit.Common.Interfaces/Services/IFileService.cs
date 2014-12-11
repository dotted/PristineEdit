namespace BadMechanics.PristineEdit.Common.Interfaces.Services
{
    using System.IO;

    public interface IFileService
    {
        Stream OpenFile(string defaultExtension, string filter);
        Stream SaveFile(string defaultExtension, string filter);       
    }
}
