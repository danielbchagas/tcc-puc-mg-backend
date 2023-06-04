using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace ECommerce.Baskets.Api.Services.gRPC
{
    [Authorize]
    public class BasketGrpcService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BasketGrpcService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }
    }
}
