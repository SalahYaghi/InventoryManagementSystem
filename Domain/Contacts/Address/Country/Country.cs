using MechanicShop.Domain.Common;
using MechanicShop.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace Domain.Contacts.Address.Country
{
    public class Country : Entity
    {

        public string Name { get; set; } = string.Empty;
        private Country() { }
        private Country(Guid id , string name) : base(id) {
        
            this.Name = name;
        }
        public static Result<Country> Create(string name)
        {
            return new Country(Guid.NewGuid(), name);
        }   

        public Result<Updated> Update(string name)
        {
            Name = name;
            return Result.Updated;
        }

    }
}

