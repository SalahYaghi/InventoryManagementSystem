using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface IRedisHealthState
    {
        bool IsRedisAvailable { get; set; } 
        DateTime LastCheckedAt { get; set; }
    }
}
