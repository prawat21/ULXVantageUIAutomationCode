using System;
using System.Threading;
using TechTalk.SpecFlow;
using Vantage.Automation.PortalUITest.Context;

namespace Vantage.Automation.PortalUITest.Steps
{
    [Binding]
    public class CommonSteps
    {
        private readonly UIContext _uiContext;
        public CommonSteps(UIContext context)
        {
            _uiContext = context;
        }

        [StepDefinition("I wait (.*) seconds")]
        public void WaitSeconds(int seconds)
        {
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
        }
    }
}
