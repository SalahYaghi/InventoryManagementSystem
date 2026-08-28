using Contract.Common.Constants; 
using Contract.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Contract.Common.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            if (pageNumber < ApplicationDefaults.DefaultPageNumber)
                pageNumber = ApplicationDefaults.DefaultPageNumber;

            if (pageSize < ApplicationDefaults.MinimumPageSize)
                pageSize = ApplicationDefaults.DefaultPageSize;

            if (pageSize > ApplicationDefaults.MaximumPageSize)
                pageSize = ApplicationDefaults.MaximumPageSize;

            var totalCount = await query.CountAsync(cancellationToken);

            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedList<T>(
                items,
                totalCount,
                pageNumber,
                pageSize,
                (int)Math.Ceiling(totalCount / (double)pageSize));
        }
    }
}
