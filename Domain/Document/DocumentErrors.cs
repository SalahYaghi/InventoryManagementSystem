using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Document
{
    public class DocumentErrors
    {
        public static readonly Error InvalidDocumentType = Error.Validation(
            "Document.InvalidDocumentType",
            "Document type is invalid.");

        public static readonly Error ImageUrlRequired = Error.Validation(
            "Document.ImageUrlRequired",
            "Image URL is required.");

        public static readonly Error ImageUrlInvalid = Error.Validation(
            "Document.ImageUrlInvalid",
            "Image URL is invalid.");
    }
}

