using Domain.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Constants
{ 
        public static class RoleConstants
        {
            public const string Admin = nameof(Role.Admin);
            public const string SalesUser = nameof(Role.SalesUser);
            public const string PurchasesUser = nameof(Role.PurchasesUser);
            public const string WarehouseUser = nameof(Role.WarehouseUser);
            public const string Viewer = nameof(Role.Viewer);
        }
  }
