using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureManager.Utility
{
    class Common
    {
        public static bool IsPathHidden(string path)
        {
            DirectoryInfo info = new DirectoryInfo(path);
            return (info.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden;
        }
    }
}
