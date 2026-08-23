using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI.Shared.Storage
{
    public static class SecurityStorage
    {

        private static readonly string Folder = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
            ,Constants.AppName);

        private static readonly string File = Path.Combine(
            Folder
            , "auth.dat");

        public static void StoreRefreshToken(string refreshToken)
        {
            Directory.CreateDirectory(Folder);
            try
            {

                byte[] plainBytes = Encoding.UTF8.GetBytes(refreshToken);

                var protectedBytes =
                    ProtectedData.Protect(plainBytes, null, DataProtectionScope.CurrentUser);

                System.IO.File.WriteAllBytes(File, protectedBytes);

            }
            catch (Exception) { } ;
        }
        public static string ReadRefreshToken()
        {
            if(!System.IO.File.Exists(File))
                return null;

            try
            {
               byte[] protectedBytes =  System.IO.File.ReadAllBytes(File);

               byte [] plainBytes = 
                    ProtectedData.Unprotect(protectedBytes ,null, DataProtectionScope.CurrentUser);

                return Encoding.UTF8.GetString(plainBytes);
            }
            catch (Exception) {

                return null;
            };
            
        }
        public static void Clear() {
            if (System.IO.File.Exists(File))
                System.IO.File.Delete(File);
        }

    }
}
