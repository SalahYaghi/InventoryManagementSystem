
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Contract.Requests.Identity
{
    public class JwtGeneratCommand { 
        
        public string email { get; set; }
        public string password { get; set; }
            
            } //: IRequest<Result<JwtDto>>;
}


