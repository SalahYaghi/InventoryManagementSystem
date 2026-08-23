using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Requests.Login
{
    public sealed record JwtRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

    }
}


