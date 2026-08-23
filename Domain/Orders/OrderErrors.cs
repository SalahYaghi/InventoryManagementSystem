using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System.Reflection.Metadata.Ecma335;

namespace Domain.Orders
{
    public static class OrderErrors
    {
        public static  Error CannotTransferFromStatusToAnother(OrderStatus from,
            OrderStatus to) => Error.Conflict("Order.TrasformationRejected" , 
                $"Can't trasnfer from order status {from.ToString()} to {to.ToString()}");


        public static readonly Error SupplierIsRequiredForPurchasesReturnOutOperatoins =
            Error.Validation("Order.SupplierIdRequired", "Supplier is required for any purchase , return out operation.");
        public static readonly Error CustomeIsRequiredForSalesReturnInOperations =
            Error.Validation("Order.CustomerIsRequired", "Customer is required for any sales , return in operation.");

        public static readonly Error DestinationWarehouseIsRequiredForTransfer =
            Error.Validation("Order.DestinationWarehouseRequired", "Destination warehouse is required for any transfer operation.");

        public static readonly Error InvalidDueDateSentItMustBeMoreThanToday =
           Error.Validation("Order.InvalidDueDate", "DueDate must be more than or equal today.");

        public static readonly Error OrderAlreadyHasIssuedLicense =
           Error.Conflict("Order.AlreadyHasInvoice", "Order already has issued licenese.");

        public static readonly Error CannotIssueInvoiceBeforeOrderCompeletion =
           Error.Validation("Order.InvalidInvoiceIssueOrder", "Cannot issue invoice without order compeletions.");

        public static readonly Error CannotCompeleteOrderBeforeDueDateComes =
           Error.Validation("Order.InvalidCompeletionOrder", "Cannot compelete the order before due date comes.");

        public static readonly Error OrderIsLocked =
           Error.Conflict("Order.OrderIsLocked", "Order is locked can't be modiefied.");

        public static readonly Error IssueInoiceInvalidForType =
            Error.Validation("Invoice.Cannot Issue Invoice For That Type Of Orders");

        public static readonly Error OrderDetailsAreRequired =
            Error.Validation("Order.OrderDetailsRequired", "Order details are required.");

        public static readonly Error InvalidOrderType =
            Error.Validation("Order.InvalidOrderType", "Order type is invalid.");

        public static readonly Error InvalidOrderStatus =
            Error.Validation("Order.InvalidOrderStatus", "Order status is invalid.");

        public static readonly Error SupplierRequired =
            Error.Validation("Order.SupplierRequired", "Supplier is required.");

        public static readonly Error CustomerRequired =
            Error.Validation("Order.CustomerRequired", "Customer is required.");

        public static readonly Error SourceWarehouseRequired =
            Error.Validation("Order.SourceWarehouseRequired", "Source warehouse is required.");

        public static readonly Error DestinationWarehouseRequired =
            Error.Validation("Order.DestinationWarehouseRequired", "Destination warehouse is required.");

        public static readonly Error NetAmountInvalid =
            Error.Validation("Order.NetAmountInvalid", "Net amount must be greater than or equal to zero.");

        public static readonly Error SubTotalAmountInvalid =
            Error.Validation("Order.SubTotalAmountInvalid", "Subtotal amount must be greater than or equal to zero.");

        public static readonly Error DiscountAmountLargerThanNet =
            Error.Validation("Order.DiscountAmountInvalid", "Discount amount must be less than or equal to sub total amount.");

        public static readonly Error InvalidOrderDetailsEmpty =
                  Error.Conflict("Order.InvalidOrderDetailsEmpty", "the order detail sent was empty.");


        public static readonly Error CannotRemoveLastOrderDetail =
                    Error.Conflict("Order.CannotRemoveLastOrderDetail", "An order must contain at least one detail line.");
        
        public static  Error DisountCannotBeAssignedToOrderType(OrderType type) => 
      
            Error.Conflict("Order.DisountCannotBeAssignedToOrderType", $"Discount cannot be assigned to order type {type.ToString()}.");

        public static readonly Error DiscountAmountInvalid =
            Error.Validation("Order.DiscountAmountInvalid", "Discount amount must be greater than or equal to zero.");

        public static readonly Error NotesTooLong =
            Error.Validation("Order.NotesTooLong", "Notes exceeds maximum length.");
    }
}

