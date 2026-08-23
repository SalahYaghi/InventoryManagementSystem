using Contract.Common.Constants;
using FluentValidation;

namespace Contract.Features.Inventory.Adjustment.Queries.GetAdjustmentDetailPaged
{
    public sealed class GetAdjustmentDetailPagedQueryValidator : AbstractValidator<GetAdjustmentDetailPagedQuery>
    {
        public GetAdjustmentDetailPagedQueryValidator()
        {
           }
    }
}

