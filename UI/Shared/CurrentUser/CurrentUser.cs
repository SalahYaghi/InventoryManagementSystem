using Infrastructure.Identity;
using OldContract.Features.User.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI.Shared.CurrentUser
{
    public static class CurrentUser
    {
        public static UserDto User { get; set;  }
        public static string Jwt { get; set; }
      

    }
}

