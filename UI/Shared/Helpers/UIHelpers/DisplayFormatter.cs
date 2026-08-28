using System;
using System.Globalization;

namespace UI.Shared.Helpers.UI_Helpers
{
    public static class DisplayFormatter
    {
        public const string EmptyPlaceholder = "—";
        public const string NotSetPlaceholder = "Not set";
        public const string NotAvailablePlaceholder = "Not available";

        public const string MoneyGridFormat = "$#,##0.00";
        public const string QuantityGridFormat = "#,##0.##";
        public const string DateGridFormat = "dd MMM yyyy";
        public const string DateTimeGridFormat = "dd MMM yyyy  HH:mm";

        private static readonly CultureInfo Culture = CultureInfo.InvariantCulture;

        public static string Money(decimal value)
        {
            return "$" + value.ToString("N2", Culture);
        }

        public static string Money(decimal? value)
        {
            return value.HasValue ? Money(value.Value) : Money(0m);
        }

        public static string MoneyOrPlaceholder(decimal? value, string placeholder = EmptyPlaceholder)
        {
            return value.HasValue ? Money(value.Value) : placeholder;
        }

        public static string Quantity(decimal value)
        {
            return value.ToString("#,##0.##", Culture);
        }

        public static string Quantity(decimal? value, string placeholder = EmptyPlaceholder)
        {
            return value.HasValue ? Quantity(value.Value) : placeholder;
        }

        public static string Count(int value)
        {
            return value.ToString("#,##0", Culture);
        }

        public static string Date(DateTime value)
        {
            return value == default(DateTime) ? EmptyPlaceholder : value.ToString(DateGridFormat, Culture);
        }

        public static string Date(DateTime? value, string placeholder = EmptyPlaceholder)
        {
            if (!value.HasValue || value.Value == default(DateTime))
                return placeholder;

            return value.Value.ToString(DateGridFormat, Culture);
        }

        public static string DateTimeValue(DateTime value)
        {
            return value == default(DateTime) ? EmptyPlaceholder : value.ToString(DateTimeGridFormat, Culture);
        }

        public static string DateTimeValue(DateTime? value, string placeholder = EmptyPlaceholder)
        {
            if (!value.HasValue || value.Value == default(DateTime))
                return placeholder;

            return value.Value.ToString(DateTimeGridFormat, Culture);
        }

        public static string DateTimeValue(DateTimeOffset value)
        {
            return value.LocalDateTime.ToString(DateTimeGridFormat, Culture);
        }

        public static string Text(string value, string placeholder = EmptyPlaceholder)
        {
            return string.IsNullOrWhiteSpace(value) ? placeholder : value.Trim();
        }

        public static string YesNo(bool value)
        {
            return value ? "Yes" : "No";
        }

        public static string ActiveInactive(bool value)
        {
            return value ? "Active" : "Inactive";
        }

        public static string Percentage(decimal value)
        {
            return value.ToString("0.##", Culture) + "%";
        }

        public static string Elapsed(DateTime? value)
        {
            if (!value.HasValue || value.Value == default(DateTime))
                return EmptyPlaceholder;

            TimeSpan difference = DateTime.Now - value.Value;

            if (difference.TotalSeconds < 0)
                return DateTimeValue(value);

            if (difference.TotalMinutes < 1)
                return "Just now";

            if (difference.TotalHours < 1)
                return ((int)difference.TotalMinutes) + " min ago";

            if (difference.TotalDays < 1)
                return ((int)difference.TotalHours) + " h ago";

            if (difference.TotalDays < 30)
                return ((int)difference.TotalDays) + " d ago";

            return DateTimeValue(value);
        }
    }
}
