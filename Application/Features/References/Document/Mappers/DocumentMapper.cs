using Domain.Document;
using Contract.Features.References.Documents.DTOs;

namespace Contract.Features.References.Documents.Mappers
{
    public static class DocumentMapper
    {
        public static DocumentDto ToDto(this Domain.Document.Document entity)
        {
            return new DocumentDto
            {
                Id = entity.Id,
                DocumentType = entity.DocumentType,
             };
        }
    }
}

