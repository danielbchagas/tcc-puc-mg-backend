namespace ECommerce.Compras.Gateway.Models
{
    public class JwtOptions
    {
        public string Secret { get; set; }
        public int Expiration { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
