using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.References.Document
{
    public static class DocumentApplicationErrors
    {
        public static Error ErrorSavingImage =>
            Error.Validation("SavingImage.ServerError",
            "An error occurred while saving the document image.");

        public static Error ImageIsRequired => 
            Error.Validation("DocumentImage.IsRequired",
            "Document image is required.");

        public static Error ImageFormattingError => 
            Error.Unexpected("DocumentImage.FormattingInvalid",
            "Document image allowed types are png and jpg.");
    }
}

