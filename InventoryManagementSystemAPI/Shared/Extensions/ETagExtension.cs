using System.Security.Cryptography;
using System.Text.Json;

namespace InventoryManagementSystemAPI.Shared.Helpers
{
    public static class ETagExtension
    {

        public static string GenerateETag(this object value)
        {

            var json = JsonSerializer.Serialize(value);
            var hashBytes = SHA256.HashData(System.Text.Encoding.UTF8.GetBytes(json));
            var newEtag = $"\"{Convert.ToHexString(hashBytes)}\"";
            return newEtag;
        }


    }
}
