using Domain.Common.Helpers;
using Inventory.Domain.Common;
using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace Domain.Products.Category
{
    public class Category : Entity
    {
        public string Name { get; private set; }
        private Category() { }
        private Category(Guid id , string name) : base(id)
        {
            Name = name;
        }

        public static Result<Category> Create(Guid id , string name)
        {
            if (string.IsNullOrWhiteSpace(name) || !ValidationHelper.IsInRange(name.Length, 1, 20))
                return Error.Validation("InvalidName", "Category name must be between 1 and 20 characters.");

            return new Category(id,name);
        }


        public Result<Updated> Update(string name)
        {
            if (string.IsNullOrEmpty(name) || !ValidationHelper.IsInRange(name.Length, 1, 20))
                return Error.Validation("InvalidName", "Category name must be between 1 and 20 characters.");

            Name = name;
            return Result.Updated;
        }

    }
}

