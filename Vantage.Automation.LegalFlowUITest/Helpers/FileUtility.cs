using System;
using System.IO;

namespace Vantage.Automation.LegalFlowUITest.Helpers
{
    public class FileUtility
    {
        public string CreateFile(string fileName, long sizeInBytes)
        {
            string path = Path.Combine(Environment.CurrentDirectory, fileName);
            using (var fs = new FileStream(path, FileMode.Create))
            {
                fs.SetLength(sizeInBytes);
            }
            return path;            
        }

        public void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
