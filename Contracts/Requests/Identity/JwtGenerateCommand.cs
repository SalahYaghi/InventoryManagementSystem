
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Contract.Requests.Identity
{
    public record JwtGeneratCommand(string email, string password); //: IRequest<Result<JwtDto>>;
}

