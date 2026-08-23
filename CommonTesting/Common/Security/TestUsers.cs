using Domain.Identity.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagement.CommonTesting.Common.Security
{

    public class TestUser {     
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class TestUsers
    {

        public TestUser admin = new TestUser()
        {
            Email = "nour@gmail.com",
            Password = "password123"
        };

        

    }
}
