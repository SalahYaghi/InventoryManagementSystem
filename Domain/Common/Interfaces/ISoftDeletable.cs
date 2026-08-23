using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common.Results.Interfaces
{
    public interface ISoftDeletable
    {
        bool? IsDeleted { get; set; }
        DateTimeOffset? DeletedAt { get; set; }
        public void Delete() { 
            
            if(IsDeleted.HasValue && IsDeleted.Value) return;

            IsDeleted = true; 
            DeletedAt = DateTimeOffset.UtcNow;
        }
        public void UndoDelete() { 
        
            IsDeleted = false;
            DeletedAt = null;

        }
    }
}
