using MechanicShop.Domain.Common;
using System;
using MechanicShop.Domain.Common.Results;
using Domain.Contacts.Address;
using Domain.Identity.Employee;

namespace Domain.Warehouses
{
    public class Warehouse : AuditableEntity
    {
        public string Name { get; private set; } = string.Empty;
        public string Code { get; private set; } = string.Empty;
        public Guid AddressId { get; private set; }
        public WarehouseStatus WarehouseStatus { get; private set; }
        public Address? Address { get; private set; }


    //    private readonly List<Employee> _employees = new();
        public List<Employee> Employees { get; set; }//=> _employees;


        private Warehouse() { }

        private Warehouse(
            Guid id,
            string name,
            string code,
            Address address,
            WarehouseStatus warehouseStatus) : base(id)
        {
            Name = name;
            Code = code;
            Address = address;
            WarehouseStatus = warehouseStatus;
        }

        public static Result<Warehouse> Create(
            Guid id,
            string name,
            string code,
            Address? address)
        {
            if (string.IsNullOrWhiteSpace(name))
                return WarehouseErrors.NameRequired;

            if (name.Length > 100)
                return WarehouseErrors.NameTooLong;

            if (string.IsNullOrWhiteSpace(code))
                return WarehouseErrors.CodeRequired;

            if (code.Length > 50)
                return WarehouseErrors.CodeTooLong;

            if (address is null)
                return WarehouseErrors.AddressRequired;


            var warehouse = new Warehouse(
                id,
                name,
                code,
                address
                , WarehouseStatus.Active
                );

            return warehouse;
        }
        public Result<Updated> Update(
            string name,
            string code,
            Address? address,
            WarehouseStatus warehouseStatus)
        {
            if (string.IsNullOrWhiteSpace(name))
                return WarehouseErrors.NameRequired;

            if (name.Length > 100)
                return WarehouseErrors.NameTooLong;

            if (string.IsNullOrWhiteSpace(code))
                return WarehouseErrors.CodeRequired;

            if (code.Length > 50)
                return WarehouseErrors.CodeTooLong;


            if (!Enum.IsDefined(typeof(WarehouseStatus), warehouseStatus))
                return WarehouseErrors.InvalidStatus;
             
            if (address is not null) {

                if (this.Address is null)
                    this.Address = address;
                else
                    {

                        var updateResult = this.Address.Update(
                            address.CountryId,
                            address.CityId,
                            address.PostalCode,
                            address.BuildingNumber,
                            address.Street,
                            address.Description
                        );
                    if (updateResult.IsError)
                    {
                        return updateResult.Errors;
                    }

                }
            }

            Name = name;
            Code = code;
            WarehouseStatus = warehouseStatus;


            return Result.Updated;
        }

    }
}

