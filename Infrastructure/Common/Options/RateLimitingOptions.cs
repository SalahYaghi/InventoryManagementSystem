using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Options
{
   
    public sealed class RateLimitingOptions
    {
        public GlobalRateLimiterOptions Global { get; set; } = new();

        public AuthRateLimiterOptions Auth { get; set; } = new();
    }

    public sealed class GlobalRateLimiterOptions
    {
        public int PermitLimit { get; set; }

        public int WindowInMinutes { get; set; }

        public int SegmentsPerWindow { get; set; }

        public int QueueLimit { get; set; }

        public bool AutoReplenishment { get; set; }
    }

    public sealed class AuthRateLimiterOptions
    {
        public int PermitLimit { get; set; }

        public int WindowInMinutes { get; set; }

        public int QueueLimit { get; set; }
    }
}
