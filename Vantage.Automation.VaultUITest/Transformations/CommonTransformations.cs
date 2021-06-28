using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow;
using Vantage.Automation.VaultUITest.Helpers;

namespace Vantage.Automation.VaultUITest.Transformations
{
    [Binding]
    public class CommonTransformations
    {
        [StepArgumentTransformation]
        public IList<string> StringsListTransformation(Table inputTable)
        {
            return inputTable.Rows.Select(r => r.Values.First()).ToList();
        }

        [StepArgumentTransformation]
        public Dictionary<string, string> DictTransformation(Table inputTable)
        {
            StringTransformer tr = new StringTransformer();
            return inputTable.Header.ToDictionary(h => h, h => tr.Transform(inputTable.Rows[0][h]));
        }
    }
}
