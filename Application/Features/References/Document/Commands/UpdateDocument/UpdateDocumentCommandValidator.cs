using FluentValidation;

namespace Contract.Features.References.Documents.Commands.UpdateDocument
{
    public sealed class UpdateDocumentCommandValidator : AbstractValidator<UpdateDocumentCommand>
    {
        public UpdateDocumentCommandValidator()
        {
            RuleFor(x => x.Id).NotEqual(Guid.Empty);

        }
    }
}

