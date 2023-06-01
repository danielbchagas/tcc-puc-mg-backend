﻿using ECommerce.Identity.Api.Models.Request;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api.Interfaces
{
    public interface ICustomerGrpcClient
    {
        Task<Customers.Api.Protos.CreateUserResponse> Create(CustomerRequest user);
    }
}
