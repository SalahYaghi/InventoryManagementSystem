using Domain.Identity.Employee;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Parties.Employees.Commands.UpdateEmployee
{
    public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
    {
        public UpdateEmployeeCommandValidator() {

            RuleFor(e => e.jobTitle)
                .NotEmpty()
                .WithMessage("job title is required");

            RuleFor(e => e.warehouseId)
                .NotEmpty()
                .WithMessage("person is required");



        }
    }
}

