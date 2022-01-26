using System.Collections.Generic;

namespace ECommerce.Identity.Api.Models
{
    public class UserJwt
    {
        public string Token { get; set; }
        public double ExpiresIn { get; set; }
        public TokenUsuario UserToken { get; set; }
    }

    public class TokenUsuario
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> UserClaims { get; set; }
    }

    public class UserClaim
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
