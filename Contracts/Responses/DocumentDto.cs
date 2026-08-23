using System;
using Contract.Common;

namespace Contract.Responses
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public DocumentType DocumentType { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}


