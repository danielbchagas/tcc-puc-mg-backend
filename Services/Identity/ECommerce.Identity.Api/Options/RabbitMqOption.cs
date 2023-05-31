namespace ECommerce.Identity.Api.Options
{
    public class RabbitMqOption
    {
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Exchange { get; set; }
        public string Queue { get; set; }
    }
}
