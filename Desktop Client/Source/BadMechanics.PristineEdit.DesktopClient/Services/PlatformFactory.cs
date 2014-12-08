using BadMechanics.PristineEdit.DesktopClient.Services.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BadMechanics.PristineEdit.DesktopClient.Services
{
    public static class PlatformFactory
    {
        public static IFileService GetFileService()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                    return new Win32FileService();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
