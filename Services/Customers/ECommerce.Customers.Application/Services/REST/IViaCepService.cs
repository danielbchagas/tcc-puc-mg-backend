using Refit;
using System.Threading.Tasks;

namespace ECommerce.Customers.Application.Services.REST
{
    public interface IViaCepService
    {
        [Get("{zipCode}/json/")]
        Task<ViaCepResponse> Get(string zipCode);
    }

    public class ViaCepResponse
    {
        public string cep { get; set; }
        public string logradouro { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public string localidade { get; set; }
        public string uf { get; set; }
        public string ibge { get; set; }
        public string gia { get; set; }
        public string ddd { get; set; }
        public string siafi { get; set; }
    }
}
