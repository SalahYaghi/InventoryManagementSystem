using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Shared.Helpers.UI_Helpers
{
    public class TextFormattingHelper
    {

        public static string JoinString(string[] names) {

            return string.Join( "" , 
                names.Select(n => n + " ").Where(n => !string.IsNullOrEmpty(n)).ToArray());
        }
    }
}

