using FluentValidation;

namespace Contract.Features.References.Documents.Commands.CreateDocument
{
    public sealed class CreatePersonDocumentCommandValidator : AbstractValidator<CreatePersonDocumentCommand>
    {
        public CreatePersonDocumentCommandValidator()
        {
            RuleFor(x => x.Document).SetValidator(new CreateDocumentCommandValidator());
            RuleFor(x => x.PersonId).NotEmpty();

        }
    }
}

