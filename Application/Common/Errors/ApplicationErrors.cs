using MechanicShop.Domain.Common.Results;

namespace Contract.Common.Errors
{
    public static class ApplicationErrors
    {

        public static readonly Error UserWithEmailAlreadyExists =
            Error.Conflict("User.EmailExist", "User with email already exists.");

        public static readonly Error UserWithUsernameAlreadyExists =
            Error.Conflict("User.UsernameExist", "User with username already exists.");

        public static readonly Error UserNotFound =
            Error.NotFound("User.NotFound", "User was not found.");

        public static readonly Error PasswordIsWrong =
            Error.Validation("User.WrongPassword", "Wrong password was sent.");

        public static readonly Error NewPasswordMustDiffer =
            Error.Validation("User.NewPasswordMustDiffer", "New password must be different from the current password.");

        public static readonly Error EmployeeAlreadyHasUser =
            Error.Conflict("User.EmployeeAlreadyHasUser", "This employee already has a user account.");

        public static readonly Error EmployeeNotFound =
            Error.NotFound("Employee.NotFound", "Employee was not found.");

        public static readonly Error EmployeeHasUsers =
            Error.Conflict("Employee.HasUsers", "Employee cannot be deleted while a user account is linked to it.");

        public static readonly Error PersonAlreadyHasEmployee =
            Error.Conflict("Person.AlreadyHasEmployee", "This person is already registered as an employee.");


        public static readonly Error WarehouseNotFound =
            Error.NotFound("Warehouse.NotFound", "Warehouse is not found.");

        public static readonly Error WarehouseInActive =
            Error.Conflict("Warehouse.InActive", "Warehouse is inactive.");

        public static readonly Error WarehouseStockNotFound =
            Error.NotFound("WarehouseStock.NotFound", "Warehouse stock was not found.");

        public static readonly Error WarehouseHasStock =
            Error.Conflict("Warehouse.HasStock", "Warehouse cannot be deleted while it still holds stock.");

        public static readonly Error WarehouseHasEmployees =
            Error.Conflict("Warehouse.HasEmployees", "Warehouse cannot be deleted while employees are assigned to it.");


        public static readonly Error ProductNotFound =
            Error.NotFound("Product.NotFound", "Product is not found.");

        public static readonly Error UpdateOccursOnProducts =
            Error.Conflict("Product.UpdateOccurred", "Update occurred on products, please refresh the list.");

        public static readonly Error SKUAlreadyExits =
            Error.Conflict("Product.SKUAlreadyExists", "SKU already exists.");

        public static readonly Error CategoryHasProducts =
            Error.Conflict("Category.HasProducts", "Category cannot be deleted while products reference it.");


        public static readonly Error SupplierProductAlreadyExists =
            Error.Conflict("SupplierProduct.AlreadyExists",
                "A SupplierProduct with the same SupplierId and ProductId already exists.");

        public static readonly Error SupplierDoesNotSellProduct =
            Error.Validation("SupplierProduct.SupplierProductNotFound", "Supplier does not sell product.");

        public static readonly Error SupplierNotFound =
            Error.NotFound("Supplier.NotFound", "Supplier was not found.");

        public static readonly Error SupplierInActive =
            Error.Conflict("Supplier.InActive", "Supplier is inactive.");

        public static readonly Error SupplierCodeAlreadyExists =
            Error.Conflict("Supplier.CodeAlreadyExists", "Supplier code already exists.");

        public static readonly Error SupplierHasOrders =
            Error.Conflict("Supplier.HasOrders", "Supplier cannot be deleted while orders reference it.");


        public static readonly Error CustomerNotFound =
            Error.NotFound("Customer.NotFound", "Customer was not found.");

        public static readonly Error CustomerInActive =
            Error.Conflict("Customer.InActive", "Customer is inactive.");

        public static readonly Error CustomerCodeAlreadyExists =
            Error.Conflict("Customer.CodeAlreadyExists", "Customer code already exists.");

        public static readonly Error CustomerHasOrders =
            Error.Conflict("Customer.HasOrders", "Customer cannot be deleted while orders reference it.");


        public static readonly Error OrderNotFound =
            Error.NotFound("Order.NotFound", "Order was not found.");

        public static readonly Error UnsupportedOrderType =
            Error.Validation("Order.UnsupportedOrderType", "Order type is not supported yet.");

        public static readonly Error UnsupportedAdjustmentType =
            Error.Validation("Adjustment.UnsupportedAdjustmentType", "Adjustment type is not supported yet.");

        public static readonly Error AdjustmentNotFound =
            Error.NotFound("Adjustment.NotFound", "Adjustment was not found.");

        public static readonly Error AdjustmentDetailNotFound =
            Error.NotFound("AdjustmentDetail.NotFound", "Adjustment detail was not found.");

        public static readonly Error OrderDetailNotFound =
            Error.NotFound("OrderDetail.NotFound", "Order detail was not found.");

        public static readonly Error QuantityInvalid =
            Error.Conflict("OrderDetail.QuantityOverStock",
                "Quantity sent is over the available stock quantity.");

        public static readonly Error QuantityInvaidReservedQuanity =
            Error.Conflict("OrderDetail.QuantityOverUnreservedStock",
                "Quantity sent is over the available stock quantity; part of the stock is reserved.");

        public static readonly Error ProductAlreadyExistInOrderDetails =
            Error.Conflict("OrderDetails.ProductAlreadyExist", "Product already exists in the order details.");


        public static readonly Error NationalNoAlreadyExist =
            Error.Conflict("People.NationalNoConflict", "A person with this national number already exists.");

        public static readonly Error PersonNotFound =
            Error.NotFound("People.NotFound", "Person was not found.");

        public static readonly Error ImageNotFound =
            Error.NotFound("PersonImage.NotFound", "Image was not found.");

        public static readonly Error InvoiceNotFound =
            Error.NotFound("Invoice.NotFound", "Invoice was not found.");
    }
}
