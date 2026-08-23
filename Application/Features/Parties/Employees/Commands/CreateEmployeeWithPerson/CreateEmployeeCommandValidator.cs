using Domain.Identity.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.CreateEmployeeWithPerson
{
    public class CreateEmployeeWithPersonCommandValidator : AbstractValidator<CreateEmployeeWithPersonCommand>
    {
        public CreateEmployeeWithPersonCommandValidator() {

            RuleFor(e => e.jobTitle)
                .NotEmpty()
                .WithMessage("job title is required");
             

        }
    }
}

