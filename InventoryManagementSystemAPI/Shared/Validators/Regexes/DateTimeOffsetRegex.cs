using System.Text.RegularExpressions;

namespace InventoryManagementSystemAPI.Shared.Validators.Regexes
{
    public static class DateTimeOffsetRegex
    {
        private static readonly Regex _regex = new Regex(
            $@"(Z|[+-]\d{2}:\d{2})",
            RegexOptions.Compiled);

        public static bool IsValidDateTimeOffset(string value) { 
  
            return _regex.IsMatch(value);
        }
    }

}
