using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contract.Requests.Documents
{
    public class CreatePersonDocumentRequest
    {
        [Required(ErrorMessage = "PersonId is required.")]
        public Guid PersonId { get; set; }

        [Required(ErrorMessage = "Document is required.")]
         public CreateDocumentRequest Document { get; set; } = new();
    }
}


