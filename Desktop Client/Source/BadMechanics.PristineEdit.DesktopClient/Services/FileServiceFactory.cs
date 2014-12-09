namespace BadMechanics.PristineEdit.DesktopClient.Services
{
    using System;

    using BadMechanics.PristineEdit.DesktopClient.Services.Win32;

    public static class FileServiceFactory
    {
        public static FileServiceClient GetFileService()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                    return new FileServiceClient(new Win32FileService());
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
