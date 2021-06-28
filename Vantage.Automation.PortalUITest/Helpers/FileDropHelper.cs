using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Vantage.Automation.PortalUITest.Context;

namespace Vantage.Automation.PortalUITest.Helpers
{
    // Need this utility to drag and drop files
    // Code was taken from StackExchange:
    // https://sqa.stackexchange.com/questions/22191
    public class FileDropHelper
    {
        private readonly UIContext _uiContext;

        public FileDropHelper(UIContext context)
        {
            _uiContext = context;
        }

        private const string JS_DROP_FILE =
                        "var target = arguments[0]," +
                        "    offsetX = arguments[1]," +
                        "    offsetY = arguments[2]," +
                        "    document = target.ownerDocument || document," +
                        "    window = document.defaultView || window;" +
                        "" +
                        "var input = document.createElement('INPUT');" +
                        "input.type = 'file';" +
                        "input.style.display = 'none';" +
                        "input.onchange = function () {" +
                        "  var rect = target.getBoundingClientRect()," +
                        "      x = rect.left + (offsetX || (rect.width >> 1))," +
                        "      y = rect.top + (offsetY || (rect.height >> 1))," +
                        "      dataTransfer = { files: this.files };" +
                        "" +
                        "  ['dragenter', 'dragover', 'drop'].forEach(function (name) {" +
                        "    var evt = document.createEvent('MouseEvent');" +
                        "    evt.initMouseEvent(name, !0, !0, window, 0, 0, 0, x, y, !1, !1, !1, !1, 0, null);" +
                        "    evt.dataTransfer = dataTransfer;" +
                        "    target.dispatchEvent(evt);" +
                        "  });" +
                        "" +
                        "  setTimeout(function () { document.body.removeChild(input); }, 25);" +
                        "};" +
                        "document.body.appendChild(input);" +
                        "return input;";

        public void DropFile(string filePath, IWebElement target, int offsetX, int offsetY)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_uiContext.Driver;
            WebDriverWait wait = new WebDriverWait(_uiContext.Driver, TimeSpan.FromSeconds(30));

            var input = (IWebElement)jse.ExecuteScript(JS_DROP_FILE, target, offsetX, offsetY);
            input.SendKeys(filePath);
            // TODO: disabling the Obsolete warning for ExpectedConditions
            // but may need to use updated package/method in future
            #pragma warning disable 0618
            wait.Until(ExpectedConditions.StalenessOf(input));
            #pragma warning restore 0618
        }
    }
}
