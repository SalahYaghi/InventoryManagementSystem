using Domain.Common.Helpers;
using Domain.Identity.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
        public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
        {
        public CreateUserCommandValidator()
        {

            RuleFor(u => u.email)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidateEmail(e))
                   .WithMessage(UserErrors.EmailNotValid.Description);
            RuleFor(u => u.username)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidateUsername(e))
                   .WithMessage(UserErrors.InvalidUsernameSent.Description);
            RuleFor(u => u.password)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidatePassword(e))
                   .WithMessage(UserErrors.InvalidPasswordSent.Description);

          



        }
    }
}

