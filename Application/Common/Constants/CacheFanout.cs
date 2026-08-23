namespace Contract.Common.Constants
{
    public static class CacheFanout
    {
        private static readonly IReadOnlyDictionary<string, string[]> Dependents =
            new Dictionary<string, string[]>(StringComparer.Ordinal)
            {
                 [CacheEntities.Customer] = new[] { CacheEntities.Order },

                  [CacheEntities.Supplier] = new[] { CacheEntities.Order, CacheEntities.SupplierProduct },

                 [CacheEntities.Warehouse] = new[]
                {
                    CacheEntities.Order, CacheEntities.WarehouseStock, CacheEntities.Employee
                },

                 [CacheEntities.Category] = new[] { CacheEntities.Product, CacheEntities.WarehouseStock },

                 [CacheEntities.Product] = new[] { CacheEntities.WarehouseStock, CacheEntities.SupplierProduct },

                 [CacheEntities.Person] = new[] { CacheEntities.Employee, CacheEntities.User },

                 [CacheEntities.Employee] = new[] { CacheEntities.User },

                 [CacheEntities.Address] = new[]
                {
                    CacheEntities.Customer, CacheEntities.Supplier,
                    CacheEntities.Person, CacheEntities.Warehouse
                },
                [CacheEntities.ContactInfo] = new[]
                {
                    CacheEntities.Customer, CacheEntities.Supplier, CacheEntities.Person
                },

                 [CacheEntities.City] = new[]
                {
                    CacheEntities.Address, CacheEntities.Customer,
                    CacheEntities.Supplier, CacheEntities.Person, CacheEntities.Employee
                },
                [CacheEntities.Country] = new[]
                {
                    CacheEntities.Address, CacheEntities.Customer,
                    CacheEntities.Supplier, CacheEntities.Person, CacheEntities.Employee
                },

                 [CacheEntities.Adjustment] = new[]
                {
                    CacheEntities.AdjustmentDetail, CacheEntities.WarehouseStock, CacheEntities.Product
                },
                [CacheEntities.AdjustmentDetail] = new[]
                {
                    CacheEntities.Adjustment, CacheEntities.WarehouseStock, CacheEntities.Product
                },

                [CacheEntities.Order] = new[]
                {
                    CacheEntities.OrderDetail, CacheEntities.WarehouseStock,
                    CacheEntities.Product, CacheEntities.Invoice
                },
                [CacheEntities.OrderDetail] = new[]
                {
                    CacheEntities.Order, CacheEntities.WarehouseStock,
                    CacheEntities.Product, CacheEntities.Invoice
                },

                [CacheEntities.Invoice] = new[] { CacheEntities.Order },
            };

        public static string[] Expand(params string[] tags)
        {
            if (tags is null || tags.Length == 0)
                return Array.Empty<string>();

            var expanded = new HashSet<string>(StringComparer.Ordinal);

            foreach (var tag in tags)
            {
                expanded.Add(tag);

                if (Dependents.TryGetValue(tag, out var dependents))
                {
                    foreach (var dependent in dependents)
                        expanded.Add(dependent);
                }
            }

            return expanded.ToArray();
        }
    }
}
