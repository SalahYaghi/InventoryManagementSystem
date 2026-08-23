using Domain.Common.Helpers;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Document
{
    public class Document : AuditableEntity
    {
        public DocumentType DocumentType { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        private Document() { }
        private Document(Guid id ,DocumentType documentType, 
            string imageUrl) : base(id)   
        {
            DocumentType = documentType;
            ImageUrl = imageUrl;
        }

        public static Result<Document> Create(Guid id, DocumentType documentType, string imageUrl)
        {
            if (!Enum.IsDefined(typeof(DocumentType), documentType))
                return DocumentErrors.InvalidDocumentType;

            if (!ValidationHelper.IsValidImageUrlOrPath(imageUrl))
                return DocumentErrors.ImageUrlInvalid;

            var document = new Document(id, documentType, imageUrl);

            return document;
        }
        public Result<Updated> Update(DocumentType documentType, string imageUrl)
        {
            if (!Enum.IsDefined(typeof(DocumentType), documentType))
                return DocumentErrors.InvalidDocumentType;

            if (!string.IsNullOrEmpty(imageUrl) && !ValidationHelper.IsValidImageUrlOrPath(imageUrl))
                return DocumentErrors.ImageUrlInvalid;

            DocumentType = documentType;

            if(!string.IsNullOrEmpty(imageUrl))
            ImageUrl = imageUrl;

            return Result.Updated;
        }

    }
}

