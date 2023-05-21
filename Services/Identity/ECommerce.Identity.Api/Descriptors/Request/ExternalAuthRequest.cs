namespace ECommerce.Identity.Api.Descriptors.Request
{
    public class ExternalAuthRequest
    {
        public string Provider { get; set; }
        public string IdToken { get; set; }
    }
}
