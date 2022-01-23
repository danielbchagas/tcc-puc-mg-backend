﻿using ECommerce.Compras.Gateway.Models.Pedido;
using Refit;
using System;
using System.Threading.Tasks;

namespace ECommerce.Compras.Gateway.Interfaces
{
    public interface IPedidoService
    {
        [Post("/api/pedidos")]
        Task<IApiResponse> Create(PedidoDto pedido, [Authorize("Bearer")] string token);

        [Get("/api/pedidos/{id}")]
        Task<ApiResponse<PedidoDto>> Get(Guid id, [Authorize("Bearer")] string token);
    }
}
