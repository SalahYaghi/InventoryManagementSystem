using System.Text.Json;
using System.Text.Json.Serialization;

namespace InventoryManagementSystemAPI.Shared.Converters
{
    using System.Globalization;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    public sealed class FlexibleDateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private static readonly string[] Formats =
        {
        "yyyy-MM-dd",
        "yyyy-MM-ddTHH:mm:ss",
        "yyyy-MM-ddTHH:mm:ss.FFFFFFF",
        "yyyy-MM-ddTHH:mm:ssK",
        "yyyy-MM-ddTHH:mm:ss.FFFFFFFK"
    };

        public override DateOnly Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.String)
                throw new JsonException("DateOnly value must be a string.");

            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
                throw new JsonException("DateOnly value cannot be empty.");

             if (DateOnly.TryParseExact(
                    value,
                    "yyyy-MM-dd",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var dateOnly))
            {
                return dateOnly;
            }

             if (DateTime.TryParse(
                    value,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind,
                    out var DateTimeOffset))
            {
                return DateOnly.FromDateTime(DateTimeOffset);
            }

            throw new JsonException($"Invalid DateOnly value: {value}");
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateOnly value,
            JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
     
}
}

