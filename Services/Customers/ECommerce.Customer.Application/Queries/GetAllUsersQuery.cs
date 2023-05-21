using ECommerce.Customers.Domain.Models;
using MediatR;
using System.Collections.Generic;

namespace ECommerce.Customers.Application.Queries
{
    public class GetAllUsersQuery : IRequest<IList<User>>
    {
    }
}
