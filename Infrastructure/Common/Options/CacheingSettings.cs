using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Common.Options
{
    public class CachingSettings
    {
        public int DefaultCacheExpirationMinutes { get; set; }
        public int DefaultLocalCacheExpirationMinutes { get; set; }
        public int MaximumPayloadBytes { get; set; }
        public int MaximumKeyLength { get; set; }
        public RedisSettings Redis { get; set; } = new();
        public OutputCacheSettings OutputCache { get; set; } = new();
    }

    public class OutputCacheSettings
    {
        public int DefaultCacheExpirationSeconds { get; set; }
        public int MaxbodySize { get; set; }
        public int MaxCacheSize { get; set; }

    }
    public class RedisSettings
    { 
        public int SyncTimeout { get; set; }
        public int AsyncTimeout { get; set; }
        public bool AbortOnConnectFail { get; set; }
        public int ConnectRetry  {get;set;}
        public int ConnectTimeout { get; set; }
        public string ConnectionString { get; set; } = string.Empty;
        public string InstanceName { get; set; } = string.Empty;

    }
}
