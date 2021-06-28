using Newtonsoft.Json;

namespace Vantage.Automation.VaultUITest.Models
{
    public abstract class BaseModel
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
