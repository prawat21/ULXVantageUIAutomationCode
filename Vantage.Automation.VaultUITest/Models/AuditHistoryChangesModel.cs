namespace Vantage.Automation.VaultUITest.Models
{
    public class AuditHistoryChangesModel : BaseModel
    {
        public string ChangedField { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }
    }
}
