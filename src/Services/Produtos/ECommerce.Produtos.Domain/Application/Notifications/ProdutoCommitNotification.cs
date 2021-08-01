﻿using MediatR;
using System;

namespace ECommerce.Produtos.Domain.Application.Notifications
{
    public class ProdutoCommitNotification : INotification
    {
        public ProdutoCommitNotification(Guid produtoId, Guid usuarioId)
        {
            Momento = DateTime.Now;
            ProdutoId = produtoId;
            UsuarioId = usuarioId;
        }

        public DateTime Momento { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Guid UsuarioId { get; set; }
    }
}
