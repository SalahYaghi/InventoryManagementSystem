using System;
using System.ComponentModel.DataAnnotations;
using Contract.Common;
using Microsoft.AspNetCore.Http;
namespace Contract.Requests.Documents
{
public class UpdateDocumentRequest
{
    public Guid Id { get; set; }

    public DocumentType DocumentType { get; set; }

    //[Required(ErrorMessage = "Image is required.")]
    public IFormFile? Image { get; set; } = null;
}
}

