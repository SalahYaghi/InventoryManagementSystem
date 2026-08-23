using FluentValidation;

namespace Contract.Features.Inventory.Warehouses.Commands.CreateWarehouse
{
    public sealed class CreateWarehouseCommandValidator : AbstractValidator<CreateWarehouseCommand>
    {
        public CreateWarehouseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Code).NotEmpty().MaximumLength(50);
            RuleFor(x => x.Address).NotNull()
                .WithErrorCode("Warehouse.Address.Invalid");
        }
    }
}

