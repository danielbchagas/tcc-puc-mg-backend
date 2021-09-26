using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ECommerce.Compras.Gateway.Services
{
    public class BaseService
    {
        protected readonly HttpClient _client;

        public BaseService(HttpClient client)
        {
            _client = client;
        }

        protected JsonSerializerOptions GetOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                ReferenceHandler = ReferenceHandler.Preserve
            };

            return options;
        }
    }
}
