using System;
using System.ComponentModel.DataAnnotations;
 namespace Contract.Requests.Documents
{
public class UpdateDocumentRequest
{
    public Guid Id { get; set; }

    public DocumentType DocumentType { get; set; }

    [Required(ErrorMessage = "Image is required.")]
    public byte[] Image { get; set; } = null;
}
}


