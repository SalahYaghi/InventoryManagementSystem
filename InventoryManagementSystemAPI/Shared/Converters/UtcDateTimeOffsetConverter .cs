using InventoryManagementSystemAPI.Shared.Interfaces;
using InventoryManagementSystemAPI.Shared.Validators.Regexes;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static QuestPDF.Helpers.Colors;

namespace InventoryManagementSystemAPI.Shared.Converters
{
    public class UtcDateTimeOffsetConverter(IHttpContextAccessor httpContext)  : JsonConverter<DateTimeOffset>
    {
        
        public override DateTimeOffset Read(ref Utf8JsonReader reader, 
            Type typeToConvert, JsonSerializerOptions options)
        {
            var raw = reader.GetString();
           
            if (string.IsNullOrEmpty(raw))
                throw new JsonException("Date-time is required");

            DateTimeOffset value = DateTimeOffset.Now;

            if (!DateTimeOffset.TryParse(raw, CultureInfo.InvariantCulture,
                   DateTimeStyles.RoundtripKind, out value))
            {
                throw new JsonException("Invalid date-time format.");

            }
            if (!DateTimeOffsetRegex.IsValidDateTimeOffset(raw))
            {

                var offset = GetOffsert();
                value = value.ToOffset(offset);
            }

            return value.ToUniversalTime();
        }

        private TimeSpan GetOffsert() {


            var service = httpContext.HttpContext?.RequestServices.GetRequiredService<UserTimeZone>();
            var timeZone = service == null ? TimeSpan.Zero : service.Offset;

            return timeZone;

        }
        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            value = value.ToOffset(GetOffsert());

            writer.WriteStringValue((value).ToString("O"));
        }
    }
}
