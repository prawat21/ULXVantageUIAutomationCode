using System.Collections.Generic;

namespace Vantage.Automation.VaultUITest.Helpers
{
    public static class ListExtensions
    {
        public static void AddIfNull<T>(this List<string> target, T item, string message)
        {
            if (item == null)
            {
                target.Add(message);
            }
        }

        public static void AddIfNotNull<T>(this List<T> target, T item)
        {
            if (item != null)
            {
                target.Add(item);
            }
        }

        public static void AddIfTrue<T>(this List<T> target, bool condition, T item)
        {
            if (condition)
            {
                target.Add(item);
            }
        }

        public static void AddIfFalse<T>(this List<T> target, bool condition, T item)
        {
            if (!condition)
            {
                target.Add(item);
            }
        }
    }
}
