using System.ComponentModel.DataAnnotations;
using Contract.Common;
using Microsoft.AspNetCore.Http;
namespace Contract.Requests.Documents
{
public class CreateDocumentRequest
{
    public DocumentType DocumentType { get; set; }

        public IFormFile DocumentImage { get; set; } = default!;
}
}


