using System;
using System.Configuration;

namespace UI.Shared.Storage
{
    public static class ConfigurationManagement
    {
        private const string InventoryApiUrlKey = "inventoryApiUrl";

        public static string GetStoredEmail()
        {
            return RegistryStorage.GetEmail();
        }

        public static void StoreEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return;

            RegistryStorage.SaveEmail(email.Trim());
        }

        public static void ResetEmail()
        {
            RegistryStorage.DeleteEmail();
        }

        public static string GetInventoryApiUrl()
        {
            string url = ConfigurationManager.AppSettings[InventoryApiUrlKey];

            if (string.IsNullOrWhiteSpace(url))
                throw new ConfigurationErrorsException(
                    "The application setting '" + InventoryApiUrlKey + "' is missing from App.config.");

            return url.Trim();
        }
    }
}
