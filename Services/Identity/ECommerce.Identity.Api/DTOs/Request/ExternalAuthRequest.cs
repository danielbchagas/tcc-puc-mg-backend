namespace ECommerce.Identity.Api.DTOs.Request
{
    public class ExternalAuthRequest
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
