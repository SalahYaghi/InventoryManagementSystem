using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Options
{
    public class AppSettings
    {
        public TimeSpan OpenAt { get; set; }
        public TimeSpan CloseAt { get; set; }

        
        public int OrderingCancellationFrequecyCheckInMinutes { get; set; }


        public int RedisConnectionHealthCheckMinutes { get; set; }

        public int OrderingCancellationThresholdMinutes { get; set; }
        public int RefreshTokenRevokerFrequentCheckInMinutes { get; set; }

    }
}
