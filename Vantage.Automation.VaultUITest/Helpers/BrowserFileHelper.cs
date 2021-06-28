using System;
using System.IO;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class BrowserFileHelper
    {
        public int GetDownloadFolderFileCount()
        {
            string downloadFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");

            if (!Directory.Exists(downloadFolder))
            {
                throw new Exception($"Could not find download folder at {downloadFolder}");
            }

            return Directory.GetFiles(downloadFolder).Length;
        }
    }
}
