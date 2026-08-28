using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelSystemUI.HttpClients
{
    using System;
    using System.Security.Policy;

    namespace InventorySystemUI.HttpClients
    {
        public static class Routes
        {
            private static string BaseRoute = "api/v1.0";

            public static class Entities
            {
                public static class Dashboard
                {
                    public static string DashboardRoute = $"{BaseRoute}/dashboard";
                }
                public static class Jwt
                {
                    public static string JwtRoute = $"{BaseRoute}/identity";

                    public static string GeneareJwtRequest() {

                        return $"{JwtRoute}/jwt/generate";
                    }
                    public static string GeneareJwtByRefreshTokenRequest()
                    {

                        return $"{JwtRoute}/jwt/refresh";
                    }
                }

                public static class User
                {
                    public static string UserRoute = $"{BaseRoute}/users";

                    public static string GetById(Guid userId) =>
                        $"{UserRoute}/{userId}";

                    public static string UpdatePassword(Guid userId) =>
                                            $"{UserRoute}/{userId}/password";

                    public static string GetByEmail(string email ) =>
                        $"{UserRoute}/{email}";

                    public static string GetAll() =>
                        $"{UserRoute}/";
                }

                public static class People
                {
                    public static string PeopleRoute = $"{BaseRoute}/people";

                    public static string GetById(Guid personId) =>
                        $"{PeopleRoute}/{personId}";

                    public static string Image(Guid personId) =>
                        $"{PeopleRoute}/{personId}/image";

                    public static string Document(Guid personId) =>
                        $"{PeopleRoute}/{personId}/document";
                }
                public static class Employees
                {
                    public static string EmployeeeRoute = $"{BaseRoute}/employees";

                    public static string GetById(Guid employeeId) =>
                        $"{EmployeeeRoute}/{employeeId}";

                }

                public static class Products
                {
                    public static string ProductsRoute = $"{BaseRoute}/products";

                    public static string GetById(Guid productId) =>
                        $"{ProductsRoute}/{productId}";

                    public static string Image(Guid productId) =>
                        $"{ProductsRoute}/{productId}/image";

                    public static string Images(Guid productId) =>
                        $"{ProductsRoute}/{productId}/images";
                }

                public static class Categories
                {
                    public static string CategoriesRoute = $"{BaseRoute}/categories";

                    public static string GetById(Guid categoryId) =>
                        $"{CategoriesRoute}/{categoryId}";
                }

                public static class Customers
                {
                    public static string CustomersRoute = $"{BaseRoute}/customers";

                    public static string GetById(Guid customerId) =>
                        $"{CustomersRoute}/{customerId}";
                }

                public static class Suppliers
                {
                    public static string SuppliersRoute = $"{BaseRoute}/suppliers";

                    public static string GetById(Guid supplierId) =>
                        $"{SuppliersRoute}/{supplierId}";

                    public static string Products(Guid supplierId) =>
                        $"{SuppliersRoute}/{supplierId}/products";

                    public static string Product(Guid supplierId, Guid productId) =>
                        $"{SuppliersRoute}/{supplierId}/products/{productId}";
                }

                public static class Warehouses
                {
                    public static string WarehousesRoute = $"{BaseRoute}/warehouses";

                    public static string GetById(Guid warehouseId) =>
                        $"{WarehousesRoute}/{warehouseId}";
                }

                public static class WarehouseStocks
                {
                    public static string WarehouseStocksRoute = $"{BaseRoute}/warehouse-stocks";

                    public static string ByWarehouse(Guid warehouseId) =>
                        $"{WarehouseStocksRoute}/{warehouseId}";

                    public static string GetById(Guid warehouseStockId) =>
                        $"{WarehouseStocksRoute}/{warehouseStockId}";

                    public static string MinimumLevel(Guid warehouseStockId) =>
                        $"{WarehouseStocksRoute}/{warehouseStockId}/minimum-level";
                }

                public static class Orders
                {
                    public static string OrdersRoute = $"{BaseRoute}/orders";
                    public static string OrderDetailsRoute = $"{OrdersRoute}/order-details";

                    public static string GetById(Guid orderId) =>
                        $"{OrdersRoute}/{orderId}";

                    public static string Details(Guid orderId) =>
                        $"{OrdersRoute}/{orderId}/order-details";

                    public static string DetailById(Guid detailId) =>
                        $"{OrderDetailsRoute}/{detailId}";

                    public static string Status(Guid orderId) =>
                        $"{OrdersRoute}/{orderId}/status";
                }

                public static class Adjustments
                {
                    public static string AdjustmentsRoute = $"{BaseRoute}/Adjustments";
                    public static string AdjustmentDetailsRoute = $"{AdjustmentsRoute}/adjustment-details";

                    public static string GetById(Guid adjustmentId) =>
                        $"{AdjustmentsRoute}/{adjustmentId}";

                    public static string Details(Guid adjustmentId) =>
                        $"{AdjustmentsRoute}/{adjustmentId}/adjustment-details";

                    public static string DetailById(Guid detailId) =>
                        $"{AdjustmentDetailsRoute}/{detailId}";

                    public static string Status(Guid adjustmentId) =>
                        $"{AdjustmentsRoute}/{adjustmentId}/status";
                }

                public static class Invoices
                {
                    public static string InvoicesRoute = $"{BaseRoute}/Invoices";

                    public static string GetById(Guid invoiceId) =>
                        $"{InvoicesRoute}/{invoiceId}";
                    public static string GetPdfById(Guid invoiceId) =>
                        $"{InvoicesRoute}/{invoiceId}/pdf";

                }

                public static class Addresses
                {
                    public static string AddressesRoute = $"{BaseRoute}/addresses";

                    public static string GetById(Guid addressId) =>
                        $"{AddressesRoute}/{addressId}";
                }

                public static class ContactInfos
                {
                    public static string ContactInfosRoute = $"{BaseRoute}/contact-infos";

                    public static string GetById(Guid contactInfoId) =>
                        $"{ContactInfosRoute}/{contactInfoId}";
                }

                public static class Countries
                {
                    public static string CountriesRoute = $"{BaseRoute}/countries";

                    public static string GetById(Guid countryId) =>
                        $"{CountriesRoute}/{countryId}";

                    public static string GetAllCities(Guid countryId) {

                        return $"{CountriesRoute}/{countryId}/cities";
                    }

                }

                public static class Cities
                {
                    public static string CitiesRoute = $"{BaseRoute}/cities";

                    public static string GetById(Guid cityId) =>
                        $"{CitiesRoute}/{cityId}";
                }

                public static class Documents
                {
                    public static string DocumentsRoute = $"{BaseRoute}/documents";
                    public static string Image(Guid documentId) =>
    $"{DocumentsRoute}/{documentId}/image";

                    public static string GetById(Guid documentId) =>
                        $"{DocumentsRoute}/{documentId}";
                }
            }

            public static string WithPaging(string route, int pageNumber = 1, int pageSize = 10)
            {
                return $"{route}?pageNumber={pageNumber}&pageSize={pageSize}";
            }
        }
    }
}
