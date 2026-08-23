using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class RedisHealthState : IRedisHealthState
    {
        public bool IsRedisAvailable { get;  set; }
        public DateTime LastCheckedAt { get;  set; }
     }
}
