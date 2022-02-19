using ECommerce.Ordering.Gateway.Interfaces;
using System;
using System.Threading.Tasks;

namespace ECommerce.Ordering.Gateway.Services.gRPC
{
    public class CustomerGrpcClient : ICustomerGrpcClient
    {
        private readonly ECommerce.Customer.Api.Protos.Customer.CustomerClient _client;

        public CustomerGrpcClient(ECommerce.Customer.Api.Protos.Customer.CustomerClient client)
        {
            _client = client;
        }

        public async Task<Customer.Domain.Models.User> GetCustomer(Guid id)
        {
            var response = await _client.GetCustomerAsync(new ECommerce.Customer.Api.Protos.GetUserRequest { Id = Convert.ToString(id) });

            var customer = new ECommerce.Customer.Domain.Models.User(
                id: Guid.Parse(response.Id),
                firstName: response.Firstname,
                lastName: response.Lastname,
                document: new Customer.Domain.Models.Document(
                    number: response.Document.Number, 
                    userId: Guid.Parse(response.Document.Userid)
                ),
                email: new Customer.Domain.Models.Email(
                    address: response.Email.Address, 
                    userId: Guid.Parse(response.Email.Userid)
                ),
                phone: new Customer.Domain.Models.Phone(
                    number: response.Phone.Number, 
                    userId: Guid.Parse(response.Phone.Userid)
                ),
                address: new Customer.Domain.Models.Address(
                    firstLine: response.Address.Firstline,
                    secondLine: response.Address.Secondline,
                    city: response.Address.City,
                    zipCode: response.Address.Zipcode,
                    state: response.Address.State,
                    userId: Guid.Parse(response.Address.Userid)
                )
            );

            return customer;
        }
    }
}
