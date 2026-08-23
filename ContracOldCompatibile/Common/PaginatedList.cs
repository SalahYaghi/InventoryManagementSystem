using System;
using System.Collections.Generic;

public  class PaginatedList {
    
    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public PaginatedList( int totalCount, int pageNumber, int pageSize , int totalPages)
    {
        TotalCount = totalCount;
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalPages = totalPages;
    }
}

public sealed class PaginatedList<T> : PaginatedList
    {
        public List<T> Items { get; }
        public PaginatedList(List<T> items, int totalCount, int pageNumber, int pageSize, int totalPages) : base(totalCount, pageNumber, pageSize,  totalPages)
        {
            Items = items;
        }
    }



