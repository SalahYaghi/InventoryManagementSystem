using Domain.Customer;
using Domain.Invoices;
using Domain.Suppliers;
using Domain.Warehouses;
using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;

namespace Domain.Orders
{
    public class Order : AuditableEntity
    {
        public OrderType OrderType { get; private set; }
        public OrderStatus OrderStatus { get; private set; }
      
        public Guid? SupplierId { get; private set; }
        public Supplier? Supplier { get; private set; }
        
        public Guid? CustomerId { get; private set; }
        public Customer.Customer? Customer { get; private set; }

        public Guid? InvoiceId { get; private set; }
        public Invoice Invoice { get; private set; }
        public Guid SourceWarehouseId { get; private set; }
        public Warehouse? SourceWarehouse { get; private set; }

        public Guid? DestinationWarehouseId { get; private set; }
        public Warehouse? DestinationWarehouse { get; private set; }

        public decimal NetAmount => SubTotalAmount - (DiscountAmount ?? 0);
        public decimal SubTotalAmount => _orderDetails.Sum(o => o.TotalAmount);
        public decimal? DiscountAmount { get; private set; }
        public string? Notes { get; private set; }
        public DateTimeOffset DueDate { get; private set; }

        public Result<Created> IssueInvoice(Invoice invoice) {

            if (OrderStatus != OrderStatus.Completed)
                return OrderErrors.CannotIssueInvoiceBeforeOrderCompeletion;

            if (!CanIssueInvoiceForOrderType())
                return OrderErrors.IssueInoiceInvalidForType;

            if (this.InvoiceId == null || this.InvoiceId == Guid.Empty)
            {
                this.Invoice = invoice;
                this.InvoiceId = invoice.Id;
            }
            else
            {
                return OrderErrors.OrderAlreadyHasIssuedLicense;
            }

            return Result.Created;
        }

        public bool IsLocked => OrderStatus == OrderStatus.Completed ||
                OrderStatus == OrderStatus.Cancelled; 

        private readonly List<OrderDetail> _orderDetails = new(); 
        public IReadOnlyCollection<OrderDetail> OrderDetails => _orderDetails;

        private bool CanTransferStatus(OrderStatus status) {

            if( (status == OrderStatus.Completed || 
                status == OrderStatus.Cancelled) && OrderStatus == OrderStatus.Pending)     
                return true;

                return false;

        }

        public Result<Updated> UpdateStatus(OrderStatus status) {

            if (IsLocked)
                return OrderErrors.OrderIsLocked;


            if (!CanTransferStatus(status))
                return OrderErrors.CannotTransferFromStatusToAnother(OrderStatus , status);

            OrderStatus = status;

            return Result.Updated;
        }

        private Order() { }

        private Order(
            Guid id,
            OrderType orderType,
            OrderStatus orderStatus,
            Guid? supplierId,
            Guid? customerId,
            Guid sourceWarehouseId,
            Guid? destinationWarehouseId,
            decimal? discountAmount,
            string? notes , 
            List<OrderDetail> orderDetails, DateTimeOffset dueDate) : base(id)
        {
            OrderType = orderType;
            OrderStatus = orderStatus;
            SupplierId = supplierId;
            CustomerId = customerId;
            SourceWarehouseId = sourceWarehouseId;
            DestinationWarehouseId = destinationWarehouseId;
             DiscountAmount = discountAmount;
            Notes = notes;
            _orderDetails = orderDetails;
            DueDate = dueDate;
        }

        public static Result<Order> Create(
            Guid id,
            OrderType orderType,
            Guid? supplierId,
            Guid? customerId,
            Guid  sourceWarehouseId,
            Guid? destinationWarehouseId,
            string? notes, decimal? discountAmount,
            List<OrderDetail>orderDetails , 
            DateTimeOffset dueDate)
        {
            if (dueDate <= DateTimeOffset.UtcNow)
                return OrderErrors.InvalidDueDateSentItMustBeMoreThanToday;

            if (!Enum.IsDefined(typeof(OrderType), orderType))
                return OrderErrors.InvalidOrderType;
         
            if (supplierId.HasValue && supplierId.Value == Guid.Empty && (OrderType.Purchase == orderType || OrderType.ReturnOut == orderType))
                return OrderErrors.SupplierRequired;

            if (customerId.HasValue && customerId.Value == Guid.Empty && (OrderType.Sale == orderType || OrderType.ReturnIn == orderType))
                return OrderErrors.CustomerRequired;

            if (sourceWarehouseId == Guid.Empty)
                return OrderErrors.SourceWarehouseRequired;

            if (destinationWarehouseId.HasValue && destinationWarehouseId.Value == Guid.Empty && OrderType.Transfer == orderType)
                return OrderErrors.DestinationWarehouseRequired;

            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return OrderErrors.NotesTooLong;

            if (orderDetails.Count == 0)
                return OrderErrors.OrderDetailsAreRequired;

            if (discountAmount.HasValue && discountAmount.Value < 0)
                return OrderErrors.DiscountAmountInvalid;

            var allowed = AllowedInputs(orderType , supplierId ,customerId, destinationWarehouseId);

            if (allowed.IsError)
                return allowed.Errors;
            
            var order = new Order(
                id,
                orderType,
                OrderStatus.Pending,
                orderType ==  OrderType.Purchase  || orderType == OrderType.ReturnOut? supplierId : null,
                orderType == OrderType.Sale || orderType == OrderType.ReturnIn ? customerId : null,
                
                sourceWarehouseId,
                orderType == OrderType.Transfer ? destinationWarehouseId : null,
                orderType != OrderType.Transfer ?  discountAmount ?? 0 : null,
                notes,
                orderDetails,dueDate);

            if (order.NetAmount < 0)
                return OrderErrors.DiscountAmountLargerThanNet;

            return order;
        }

        private static Result<bool> AllowedInputs(OrderType type , 
            Guid? supplierId , Guid? customerId , Guid? destinationWarehouse) {

            if (type == OrderType.Purchase || type == OrderType.ReturnOut)
            {

                if (!supplierId.HasValue)
                    return OrderErrors.SupplierIsRequiredForPurchasesReturnOutOperatoins;

            }
            else if (type == OrderType.Sale || type == OrderType.ReturnIn)
            {

                if (!customerId.HasValue)
                    return OrderErrors.CustomeIsRequiredForSalesReturnInOperations;
            }
            else if (type == OrderType.Transfer)
            {
                if (!destinationWarehouse.HasValue)
                    return OrderErrors.DestinationWarehouseIsRequiredForTransfer;
            }
            return true;
        }

        public Result<Updated> Update(
            decimal discountAmount,
            string? notes , 
            DateTimeOffset? dueDate)
        {

            if (IsLocked)
                return OrderErrors.OrderIsLocked;
            
            if (discountAmount < 0)
                return OrderErrors.DiscountAmountInvalid;

            if (!string.IsNullOrWhiteSpace(notes) && notes.Length > 500)
                return OrderErrors.NotesTooLong;

            if (dueDate != null && dueDate < DateTimeOffset.UtcNow)
            {
                return OrderErrors.InvalidDueDateSentItMustBeMoreThanToday;
            }


            if (this.OrderType != OrderType.Transfer && discountAmount > SubTotalAmount)
            {
                return OrderErrors.DiscountAmountLargerThanNet;
            }

            DiscountAmount = this.OrderType == OrderType.Transfer ? null : discountAmount;
            Notes = notes;
            DueDate = dueDate == null ? DueDate : dueDate.Value;

            return Result.Updated;
        }


        
        public Result<InvoiceType> GetAssciatedInvoiceType() {

            if (!CanIssueInvoiceForOrderType(this.OrderType))
                return OrderErrors.IssueInoiceInvalidForType;

            switch (this.OrderType) {

                case OrderType.Purchase: 
                    return InvoiceType.Purchase;
                case OrderType.Sale:
                    return InvoiceType.Sale;
                case OrderType.ReturnIn:
                    return InvoiceType.ReturnIn;
                case OrderType.ReturnOut:
                    return InvoiceType.ReturnOut;

            }

            return OrderErrors.IssueInoiceInvalidForType;           
        }
        public static bool CanIssueInvoiceForOrderType(OrderType orderType) {

            return orderType == OrderType.Purchase ||
                orderType == OrderType.Sale || 
                orderType == OrderType.ReturnIn ||
                orderType == OrderType.ReturnOut;
        }
        public  bool CanIssueInvoiceForOrderType()
        {
            return CanIssueInvoiceForOrderType(this.OrderType);
        }

        public Result<Updated> UpdateDuedate(DateTimeOffset newDueDate) {

            if (IsLocked) return OrderErrors.OrderIsLocked;

            if (newDueDate < DateTimeOffset.UtcNow)
                return OrderErrors.InvalidDueDateSentItMustBeMoreThanToday;

            this.DueDate = newDueDate;

            return Result.Updated;
        }

        public Result<Updated> AddOrderDetail(OrderDetail detail) { 
        
            if(IsLocked) return OrderErrors.OrderIsLocked;

            if (detail == null)
                return OrderErrors.InvalidOrderDetailsEmpty;

            this._orderDetails.Add(detail);
            return Result.Updated;
        }
        public Result<Updated> RemoveOrderDetail(OrderDetail detail)
        {
            if (IsLocked) return OrderErrors.OrderIsLocked;
            if (_orderDetails.Count <= 1)
                return OrderErrors.CannotRemoveLastOrderDetail;
            this._orderDetails.Remove(detail);
            return Result.Updated;
        }
        public bool AlreadyHaveInvoice()
        {
            return this.InvoiceId.HasValue;
        }
    }
}

