using ECommerce.Customer.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Customer.Application.Queries
{
    public class GetAllUsersQuery : IRequest<IList<User>>
    {
    }
}
