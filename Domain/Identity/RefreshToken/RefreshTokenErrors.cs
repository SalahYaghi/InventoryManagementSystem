using Inventory.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Identity.RefreshToken
{
    public static class RefreshTokenErrors
    {
        public static Error UserIsRequired => Error.Validation("RefershToken.UserIsRequired", "user is required cannot skip it.");
        public static Error InvalidExpiratoinDate => Error.Validation("RefershToken.InvalidExpirationDate", "expiration date must be more than current time.");
        public static Error AlreadyRevoked => Error.Validation("RefershToken.TokenIsAlreadyRefoked", "token is already revoked and revoe it again.");

        public static Error TokenIsRequired => Error.Validation("RefershToken.TokenIsRequired" , "toekn is required cannot skip it.");
    }
}

