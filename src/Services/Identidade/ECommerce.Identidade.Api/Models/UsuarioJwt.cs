using System;
using System.Collections.Generic;

namespace ECommerce.Identidade.Api.Models
{
    public class UsuarioJwt
    {
        public string Token { get; set; }
        public double ExpiresIn { get; set; }
        public TokenUsuario TokenUsuario { get; set; }
    }

    public class TokenUsuario
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<ClaimUsuario> ClaimsUsuario { get; set; }
    }

    public class ClaimUsuario
    {
        public string Tipo { get; set; }
        public string Valor { get; set; }
    }
}
