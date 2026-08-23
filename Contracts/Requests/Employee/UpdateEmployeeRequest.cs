using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContracOldCompatibile.Requests.Employees
{
    public class UpdateEmployeeRequest
    { 
                public string jobTitle { get; set; } = string.Empty;
        public DateOnly hiringDate { get; set; }
        public Guid warehouseId { get; set; }
    }
}

