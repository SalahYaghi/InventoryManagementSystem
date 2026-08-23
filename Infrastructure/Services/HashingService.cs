using Contract.Common.Interfaces;
using Azure.Core;
using Domain.Identity.Employee;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Services
{
    public class HashingService : IHashingHelper
    {

        public  bool VerifyHashed<T>(string hashed, string text) where T : class{

            var hasher = new PasswordHasher<T>();

            var result = hasher.VerifyHashedPassword(null, hashed, text);

            return result == PasswordVerificationResult.Success;
        }

        public string Hash<T>(string text) where T : class {

            var hasher = new PasswordHasher<T>();
            var hashedText = hasher.HashPassword(null, 
                text);

            return hashedText;
        }

    }
}

