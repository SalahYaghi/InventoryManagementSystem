using Contract.Common.Errors;
using Domain.Common.Helpers;
using Domain.Identity.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() {

            RuleFor(u => u.email)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidateEmail(e))
                   .WithMessage(UserErrors.EmailNotValid.Description);
            RuleFor(u => u.username)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidateUsername(e))
                   .WithMessage(UserErrors.InvalidUsernameSent.Description);
                           
        }
    }
}

