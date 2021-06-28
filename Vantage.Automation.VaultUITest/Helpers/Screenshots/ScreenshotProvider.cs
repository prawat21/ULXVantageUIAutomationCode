using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using Vantage.Automation.VaultUITest.Context;

namespace Vantage.Automation.VaultUITest.Helpers.Screenshots
{
    public class ScreenshotProvider : IScreenshotProvider
    {
        private readonly UIContext _uiContext;

        public ScreenshotProvider(UIContext uiContext)
        {
            _uiContext = uiContext;
        }

        public string PublishScreenshot(string name, string prefix = null, string suffix = null, string parentDirectory = null)
        {
            var image = _uiContext.WebClient.Browser.Driver.TakeScreenshot();
            var resultName = string.Concat(prefix, name, suffix, ".jpeg");
            var directory = PathUtility.BuildAbsoluteUrl("{currentDir}\\..\\..\\screenshots");
            directory = CombineWithParentDirectory(directory, parentDirectory);
            EnsureDirectoryExists(directory);
            var path = Path.Combine(directory, resultName);
            image.SaveAsFile(path, ScreenshotImageFormat.Png);
            Log.TestLog.Info($"Screenshot was saved: {path}");
            return path;
        }

        private static string CombineWithParentDirectory(string screenshotFolder, string parentDirectory)
        {
            return parentDirectory == null ? parentDirectory : Path.Combine(screenshotFolder, parentDirectory);
        }

        private static void EnsureDirectoryExists(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }
    }
}
