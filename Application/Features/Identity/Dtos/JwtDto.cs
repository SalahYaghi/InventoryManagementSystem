using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Identity
{
    public sealed record JwtDto
    {
        public DateTimeOffset ExpiresOnUtc { get; set; }
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;

    }
}

