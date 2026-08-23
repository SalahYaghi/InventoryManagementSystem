using InventoryManagementSystemAPI.Shared.Interfaces;
using InventoryManagementSystemAPI.Shared.Validators.Regexes;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using static QuestPDF.Helpers.Colors;

namespace InventoryManagementSystemAPI.Shared.Converters
{
    public class UtcDateTimeConverter(IHttpContextAccessor httpContext)  : JsonConverter<DateTime>
    {

     // User sends time you must convert it to utc easily 
        public override DateTime Read(ref Utf8JsonReader reader,
      Type typeToConvert, JsonSerializerOptions options)
        {
            var raw = reader.GetString();

            if (string.IsNullOrEmpty(raw))
                throw new JsonException("Date-time is required");

            DateTime value;
         
            if (!DateTime.TryParse(raw, CultureInfo.InvariantCulture,
                   DateTimeStyles.RoundtripKind, out value))
            {
                throw new JsonException("Invalid date-time format.");
            }


            
            return value.ToUniversalTime();
        }

        private TimeSpan GetOffsert()
        {


            var service = httpContext.HttpContext?.RequestServices.GetRequiredService<UserTimeZone>();
            var timeZone = service == null ? TimeSpan.Zero : service.Offset;

            return timeZone;

        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            DateTimeOffset datetime = value.ToUniversalTime();

            datetime = datetime.ToOffset(GetOffsert()); 

            writer.WriteStringValue((datetime.DateTime).ToString("O"));
        }


    }
}
