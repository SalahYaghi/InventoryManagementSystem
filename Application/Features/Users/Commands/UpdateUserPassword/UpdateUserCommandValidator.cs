using Contract.Common.Errors;
using Domain.Common.Helpers;
using Domain.Identity.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.User.Commands.CreateUser
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator() {

            RuleFor(u => u.oldpassword)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidatePassword(e))
                   .WithMessage(UserErrors.InvalidPasswordSent.Description);

            RuleFor(u => u.newpassword)
                   .NotEmpty()
                   .Must(e => ValidationHelper.ValidatePassword(e))
                   .WithMessage(UserErrors.InvalidPasswordSent.Description);

        }
    }
}

