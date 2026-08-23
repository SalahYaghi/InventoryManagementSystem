using Domain.Identity.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.CreateEmployeeWithId
{
    public class CreateEmployeeCommandValidator : AbstractValidator<CreateEmployeeWithPersonIdCommand>
    {
        public CreateEmployeeCommandValidator() {

            RuleFor(e => e.jobTitle)
                .NotEmpty()
                .WithMessage("job title is required");

            RuleFor(e => e.personId)
                .NotEmpty()
                .WithMessage("person is required"); 

        }
    }
}

