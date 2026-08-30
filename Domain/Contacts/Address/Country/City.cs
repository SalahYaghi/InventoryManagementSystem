using Domain.Products.Category;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contacts.Address.Country
{
    public class City : Entity
    {

        public string Name { get; set; } = string.Empty;
        
        public Guid CountryId { get; set; } 

        private City() { }
        private City(Guid id, Guid countryId, string name) :base(id){
         
            Name = name;
            CountryId = countryId;
        }

        public static Result<City> Create(Guid id,Guid countryId, string name)
        {
            return new City(id,countryId, name);
        }

        public Result<Updated> Update(string name)
        {
            Name = name;
            return Result.Updated;
        }

    }
}

