using Contract.Features.Inventory.Product.Commands.CreateProduct;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contract.Features.Inventory.WarehouseStock.Commands.AddWarehouseProducts
{
    public class AddWarehourProductCommondValidator : AbstractValidator<AddWarehourProductCommand>
    {
        public AddWarehourProductCommondValidator() {


            RuleFor(v => v.WarehousesId).NotEmpty();
            RuleFor(v => v.Product).SetValidator(new CreateProductCommandValidator());
        
        }
    }
}

