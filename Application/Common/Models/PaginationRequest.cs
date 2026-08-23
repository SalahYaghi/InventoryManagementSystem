using Contract.Common.Constants;

namespace Contract.Common.Models
{
    public sealed record PaginationRequest
    {
        public int PageNumber { get; init; } = ApplicationDefaults.DefaultPageNumber;
        public int PageSize { get; init; } = ApplicationDefaults.DefaultPageSize;
    }
}

