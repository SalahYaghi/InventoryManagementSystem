using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace UI.Shared.Helpers.IO_Helper
{
    public class FileResponse
    {
        public byte[] FileBytes { get; set; }   
        public string FileName { get; set; }
        public string ContentType { get; set; }
    }
}

