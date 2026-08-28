using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Shared.Storage
{
    public static class RegistryStorage
    {
        private static readonly string AppDir = $@"Software\{Constants.AppName}";
        public static void SaveEmail(string email)
        {

            using (var registry = Registry.CurrentUser.CreateSubKey(AppDir))
            {

                if (registry == null) return;

                registry.SetValue("Email", email, RegistryValueKind.String);

            }
        }
    public static string GetEmail()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AppDir))
            {

                if (key == null)
                    return null;

                return key.GetValue("Email") as string;
            }
        }

        public static void DeleteEmail()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(AppDir, writable: true))
            {

                if (key == null)
                    return;

                key.DeleteValue("Email", throwOnMissingValue: false);

            }
        }
    }

}
