using Domain.Warehouses;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;

namespace Domain.Adjustments
{
    public class Adjustment : AuditableEntity
    {
        public DateTimeOffset? AprovedAt { get; private set; }
        public Guid WarehouseId { get; private set; }
        public Warehouse Warehouse { get; private set; }

        public AdjustmentType AdjustmentType { get; private set; }
        public AdjustmentReason AdjustmentReason { get; private set; }
        public AdjustmentStatus AdjustmentStatus { get; private set; }
        public string? Notes { get; private set; }

        private readonly List<AdjustmentDetail> _adjustmentDetails = new();
        public IReadOnlyCollection<AdjustmentDetail> AdjustmentDetails => _adjustmentDetails;

        public bool IsLocked => AdjustmentStatus == AdjustmentStatus.Approved ||
            AdjustmentStatus == AdjustmentStatus.Cancelled; 

        private Adjustment() { }

        private Adjustment(
            Guid id,
            Guid warehouseId,
            AdjustmentType adjustmentType,
            AdjustmentReason adjustmentReason,
            AdjustmentStatus adjustmentStatus,
            string? notes ,
            List<AdjustmentDetail> adjustmentDetails) : base(id)
        {
            WarehouseId = warehouseId;
            AdjustmentType = adjustmentType;
            AdjustmentReason = adjustmentReason;
            AdjustmentStatus = adjustmentStatus;
            Notes = notes;
            _adjustmentDetails = adjustmentDetails;
        }


        public static Result<AdjustmentType> DetermineAdjustmentType(AdjustmentReason reason , 
            AdjustmentType? type) {

            if (reason == AdjustmentReason.Damaged ||
                reason == AdjustmentReason.Lost ||
                reason == AdjustmentReason.Expired)
                return AdjustmentType.Decrease;

            if (reason == AdjustmentReason.ExtraFound)
                return AdjustmentType.Increase;
          
            if (type is null)
                return Error.Failure(
                    "Adjustment type must be provided for reason others.");
           
            return type;
        }
 
        public static Result<Adjustment> Create(
            Guid id,
            Guid warehouseId,
            AdjustmentReason adjustmentReason,
            List<AdjustmentDetail> adjustmentDetails,
            AdjustmentType? adjustmentType = null,
            string? notes = null)
        {
            if (warehouseId == Guid.Empty)
                return AdjustmentErrors.WarehouseRequired;

            if (adjustmentDetails.Count == 0) {

                return AdjustmentErrors.AdjustmentDetailsRequired;
            }

            if (adjustmentType is not null && 
                !Enum.IsDefined(typeof(AdjustmentType), adjustmentType))
                return AdjustmentErrors.InvalidAdjustmentType;

            if (!Enum.IsDefined(typeof(AdjustmentReason), adjustmentReason))
                return AdjustmentErrors.InvalidAdjustmentReason;

            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return AdjustmentErrors.NotesTooLong;

            var adjustmentTypeResult = DetermineAdjustmentType(adjustmentReason , 
                adjustmentType);

            if (adjustmentTypeResult.IsError)
                return adjustmentTypeResult.Errors;

            var adjustment = new Adjustment(
                id,
                warehouseId,
                 adjustmentTypeResult.Value,
                adjustmentReason,
                AdjustmentStatus.Draft,
                notes,
                adjustmentDetails);

            return adjustment;
        }


        public Result<Updated> AddAdjustmentDetail(AdjustmentDetail adjustmentDetail) {

            if (IsLocked)
                return AdjustmentErrors.AdjusmentIsLocked;

            this._adjustmentDetails.Add(adjustmentDetail);

            return Result.Updated;

        }
        public Result<Updated> UpdateStatus(
            AdjustmentStatus status)
        {
            if (!Enum.IsDefined(typeof(AdjustmentStatus), status))
                return AdjustmentErrors.InvalidAdjustmentStatus;

            if (IsLocked)
                return AdjustmentErrors.AdjusmentIsLocked;

            this.AdjustmentStatus = status;

            if(status == AdjustmentStatus.Approved)
                this.AprovedAt = DateTimeOffset.UtcNow;

            return Result.Updated;
        }


        public Result<Updated> Update(
            string? notes)
        {
            if (IsLocked)
                return AdjustmentErrors.AdjusmentIsLocked;


            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return AdjustmentErrors.NotesTooLong;

Notes = notes;

            return Result.Updated;
        }



    }
}

