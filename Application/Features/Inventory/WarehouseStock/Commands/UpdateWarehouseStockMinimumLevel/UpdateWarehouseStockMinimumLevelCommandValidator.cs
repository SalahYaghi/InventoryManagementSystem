using FluentValidation;

namespace Contract.Features.Inventory.WarehouseStocks.Commands.UpdateWarehouseStock
{
    public sealed class UpdateWarehouseStockMinimumLevelCommandValidator : AbstractValidator<UpdateWarehouseStockMinimumLevelCommand>
    {
        public UpdateWarehouseStockMinimumLevelCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.MinimumStockLevel).GreaterThanOrEqualTo(0);
        }
    }
}

