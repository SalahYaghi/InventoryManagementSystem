using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Options
{
    public  sealed record JwtOptions
    {
        public int TokenExpirationInMinutes { get; set; }
        public string Audience { get; set; } = string.Empty;
        public string Issuer { get; set; } = string.Empty;
        public string Secret {  get; set; } = string.Empty; 
        public int    RefreshTokenExpirationInDays { get; set; }

    }
}

