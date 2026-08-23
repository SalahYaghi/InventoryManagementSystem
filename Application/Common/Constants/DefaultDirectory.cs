using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Common.Constants
{
    public static class DefaultDirectory
    {
        public static string DefaultPeopleDirectory =
            Path.Combine(Environment.CurrentDirectory, "Images" , "People", "Resources");

        public static string DefaultPeopleDocumentsDirectory = 
            Path.Combine(DefaultPeopleDirectory, "Images", "Documents");
        
        public static string DefaultProductDirectory =
            Path.Combine(Environment.CurrentDirectory, "Images", "Product", "Resources");

    }
}

