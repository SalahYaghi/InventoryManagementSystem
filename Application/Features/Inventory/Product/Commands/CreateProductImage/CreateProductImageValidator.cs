using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.Product.Commands.CreateProductImage
{
    public class CreateProductImageValidator : AbstractValidator<CreateProductImageCommand>
    {
        public CreateProductImageValidator() {

            RuleFor(p => p.Image).NotNull().WithMessage("Image is required , can't leave it null");
            RuleFor(p => p.ProductId).NotEmpty().WithMessage("Product Id is required can't leave it as empty.");

        }
    }
}

