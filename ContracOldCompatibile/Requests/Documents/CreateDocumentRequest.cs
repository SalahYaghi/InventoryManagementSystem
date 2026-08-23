using System.ComponentModel.DataAnnotations;
 namespace Contract.Requests.Documents
{
public class CreateDocumentRequest
{
    public DocumentType DocumentType { get; set; }

    public byte[] DocumentImage { get; set; }
}
}


