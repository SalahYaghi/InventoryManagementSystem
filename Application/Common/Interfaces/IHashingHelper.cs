using Microsoft.AspNetCore.Identity;

namespace Contract.Common.Interfaces
{
    public interface IHashingHelper
    {

        bool VerifyHashed<T>(string hashed, string text) where T : class
       ;

        public string Hash<T>(string text) where T : class;
    }
}
