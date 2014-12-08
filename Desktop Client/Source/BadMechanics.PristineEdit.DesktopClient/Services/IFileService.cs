using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace BadMechanics.PristineEdit.DesktopClient.Services
{
    public interface IFileService
    {
        Stream OpenFile(String defaultExtension, String filter);
        Stream SaveFile(String defaultExtension, String filter);       
        
    }
}
