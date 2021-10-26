﻿using ECommerce.Pedido.Domain.Enums;
using ECommerce.Pedido.Domain.Models;
using System.Collections.Generic;
using MediatR;
using FluentValidation.Results;

namespace ECommerce.Pedido.Application.Commands
{
    public class AdicionarPedidoCommand : IRequest<ValidationResult>
    {
        public AdicionarPedidoCommand(decimal valor, StatusPedido status)
        {
            Valor = valor;
            Status = status;
        }
        
        #region Propriedades
        public decimal Valor { get; set; }
        public StatusPedido Status { get; set; }

        public Cliente Cliente { get; set; }
        public IList<Produto> Produtos { get; set; }
        #endregion
    }
}