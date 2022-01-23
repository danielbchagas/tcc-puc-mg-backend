using ECommerce.Pedido.Domain.Enums;
using ECommerce.Pedido.Domain.Models;
using System.Collections.Generic;
using MediatR;
using FluentValidation.Results;

namespace ECommerce.Pedido.Application.Commands
{
    public class CreatePedidoCommand : IRequest<ValidationResult>
    {
        public CreatePedidoCommand(decimal valor, StatusPedido status, Cliente cliente, IList<PedidoItem> itens)
        {
            Valor = valor;
            Status = status;
            Cliente = cliente;
            Itens = itens;
        }
        
        #region Propriedades
        public decimal Valor { get; set; }
        public StatusPedido Status { get; set; }

        public Cliente Cliente { get; set; }
        public IList<PedidoItem> Itens { get; set; }
        #endregion
    }
}
