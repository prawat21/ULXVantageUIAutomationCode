namespace Vantage.Automation.VaultUITest.Helpers.Screenshots
{
    public interface IScreenshotProvider
    {
        string PublishScreenshot(string name, string prefix = null, string suffix = null, string parentDirectory = null);
    }
}
