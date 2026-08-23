using FluentValidation;

namespace Contract.Features.References.Documents.Commands.CreateDocument
{
    public sealed class CreateDocumentCommandValidator : AbstractValidator<CreateDocumentCommand>
    {
        public CreateDocumentCommandValidator()
        {

            RuleFor(x => x.DocumentImage).NotEmpty();
        }
    }
}

