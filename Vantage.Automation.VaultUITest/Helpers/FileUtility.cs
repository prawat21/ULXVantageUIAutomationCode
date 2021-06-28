using System.IO;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class FileUtility
    {
        public static void CreateDirectoryIfNotExists(string path)
        {
            Directory.CreateDirectory(path);
        }

        public static void CreateFileOfSize(string path, long sizeInBytes, FileMode mode = FileMode.Create, FileAccess access = FileAccess.Write,
            FileShare share = FileShare.None)
        {
            using (var fs = new FileStream(path, mode, access, share))
            {
                fs.SetLength(sizeInBytes);
            }
        }

        public static void RemoveContents(string directory)
        {
            Log.TestLog.Info($"Trying to clean {directory} contents");
            if (Directory.Exists(directory))
            {
                DirectoryInfo di = new DirectoryInfo(directory);

                foreach (FileInfo file in di.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo dir in di.GetDirectories())
                {
                    dir.Delete(true);
                }

                Log.TestLog.Info($"{directory} is empty now");
            }
            else
            {
                Log.TestLog.Info($"Directory {directory} does not exist");
            }
        }
    }
}
