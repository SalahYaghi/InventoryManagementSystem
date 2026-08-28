using System;
using System.Collections.Generic;
using System.Linq;

namespace UI.Shared.Helpers.UI_Helpers
{
    public static class TextFormattingHelper
    {
        public static string JoinString(string[] names)
        {
            return JoinString(names, " ");
        }

        public static string JoinString(IEnumerable<string> names, string separator)
        {
            if (names == null)
                return string.Empty;

            return string.Join(separator, names
                .Where(n => !string.IsNullOrWhiteSpace(n))
                .Select(n => n.Trim())
                .ToArray());
        }

        public static string BuildFullName(string firstName, string secondName, string thirdName, string lastName)
        {
            return JoinString(new[] { firstName, secondName, thirdName, lastName }, " ");
        }

        public static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value) || maxLength <= 0)
                return string.Empty;

            value = value.Trim();

            if (value.Length <= maxLength)
                return value;

            return value.Substring(0, maxLength) + "...";
        }
    }
}
