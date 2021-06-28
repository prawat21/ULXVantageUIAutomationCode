using System;
using System.Text;
using System.Text.RegularExpressions;
using Bogus;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public class StringTransformer
    {
        public string Transform(string input)
        {
            if (!input.Contains("{"))
            {
                return input;
            }

            StringBuilder sb = new StringBuilder(input);

            string returnString = sb.Replace("{dd}", DateTime.UtcNow.ToString("dd"))
              .Replace("{MM}", DateTime.UtcNow.ToString("MM"))
              .Replace("{yyyy}", DateTime.UtcNow.ToString("yyyy"))
              .Replace("{##}", new Faker().Random.Int(10, 99).ToString())
              .Replace("{###}", new Faker().Random.Int(100, 999).ToString())
              .Replace("{####}", new Faker().Random.Int(1000, 9999).ToString())
              .Replace("{RandomPastDate}", new Faker().Date.Past().ToString("MM/dd/yyyy"))
              .Replace("{RandomFutureDate}", new Faker().Date.Future().ToString("MM/dd/yyyy"))
              .Replace("{RandomName}", new Faker().Person.FullName)
              .Replace("{RandomFirstName}", new Faker().Person.FirstName)
              .Replace("{RandomLastName}", new Faker().Person.LastName)
              .Replace("{RandomEmail}", new Faker().Person.Email)
              .Replace("{RandomPhone}", new Faker().Person.Phone)
              .Replace("{RandomUserName}", new Faker().Person.UserName)
              .Replace("{Lorem}", new Faker().Lorem.Word())
              .Replace("{Timestamp}", DateTime.Now.ToString("yyyyMMddHHmmss"))
              .ToString();

            // Replace any un-evaluated strings with Values from ConfigHelper
            return Regex.Replace(returnString, @"\{(.*?)\}", delegate (Match match)
            {
                string field = match.ToString();
                if (field.Length > 2)
                {
                    return GetConfigValue(field.Substring(1, field.Length - 2));
                }
                return field;
            });
        }

        private static string GetConfigValue(string fieldName)
        {
            try
            {
                return typeof(ConfigHelper).GetProperty(fieldName).GetValue(null).ToString();
            }
            catch
            {
                return "";
            }
        }
    }
}
