using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Shared.Storage
{
    public class ConfigurationManagement
    {

        private  static  string _emailPath => "email";


        public static string GetStoredEmail() {

            return ConfigurationManager.AppSettings[_emailPath];
        }

        public static void StoreEmail(string email)
        {
            ConfigurationManager.AppSettings[_emailPath] = email;
        }
        public static void ResetEmail( )
        {
            ConfigurationManager.AppSettings[_emailPath] = "userDefautl@gmail.com";
        }


    }
}
