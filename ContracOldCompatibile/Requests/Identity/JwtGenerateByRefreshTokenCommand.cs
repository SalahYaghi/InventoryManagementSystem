
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Contract.Requests.Identity
{
    public class JwtGenerateByRefreshTokenCommand { 
     public string refresh { get; set; }//: IRequest<Result<JwtDto>>;
        public bool loginSource { get; set; } = false;
    }
}


