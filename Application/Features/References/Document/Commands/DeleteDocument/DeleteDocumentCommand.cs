using MediatR;
using Inventory.Domain.Common.Results;

namespace Contract.Features.References.Documents.Commands.DeleteDocument
{
    public sealed record DeleteDocumentCommand(Guid Id) : IRequest<Result<Deleted>>;
}

