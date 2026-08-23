using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Person.Commands.UpdatePersonImage
{
    public class UpdatePersonImageCommandValidator : AbstractValidator<UpdatePersonImageCommand>
    {
        public UpdatePersonImageCommandValidator() { 
            RuleFor(x => x.PersonId).NotEmpty().WithMessage("PersonId is required.");
        }
    }
}

