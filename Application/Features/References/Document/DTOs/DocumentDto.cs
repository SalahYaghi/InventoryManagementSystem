namespace Contract.Features.References.Documents.DTOs
{
    public sealed record DocumentDto
    {
        public Guid Id { get; init; }
        public Domain.Document.DocumentType DocumentType { get; init; }
     }
}

