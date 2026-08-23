using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Shared.Services.Models
{
    public class ProblemDetails
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
        public Dictionary<string, object> Extensions { get; set; } 
    }
}
