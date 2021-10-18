using System;
using System.IO;

namespace Dishes.Utils
{
    public static class FilepathUtils
    {
        public static void ValidateFilepath(string filepath)
        {
            if (filepath is null)
                throw new ArgumentNullException(nameof(filepath));
            if (Directory.Exists(filepath) == false)
                throw new FileNotFoundException("File not found", filepath);
        }
    }
}
