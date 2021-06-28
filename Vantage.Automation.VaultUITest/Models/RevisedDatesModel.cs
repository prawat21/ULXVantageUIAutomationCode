using System;
using System.Linq;

namespace Vantage.Automation.VaultUITest.Models
{
    public class RevisedDatesModel : BaseModel
    {
        public DateTime? RevisedExpirationDate { get; set; }

        public DateTime? RevisedEffectiveDate { get; set; }

        public DateTime? RevisedServiceStartDate { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            var other = obj as RevisedDatesModel;
            return this.RevisedExpirationDate == other.RevisedExpirationDate
                && this.RevisedEffectiveDate == other.RevisedEffectiveDate
                && this.RevisedServiceStartDate == other.RevisedServiceStartDate;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                const int multiplier = 23;
                var props = GetType().GetProperties();

                return props.Aggregate(17, (current, prop) => current * multiplier + (prop?.GetHashCode() ?? 0));
            }
        }
    }
}
