﻿using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Cliente.Domain.Application.Commands
{
    public class AdicionarDocumentoCommand : IRequest<ValidationResult>
    {
        public AdicionarDocumentoCommand(Guid id, string numero, Guid clienteId)
        {
            Id = id;
            Numero = numero;

            ClienteId = clienteId;
        }

        public Guid Id { get; set; }
        public string Numero { get; set; }
        
        public Guid ClienteId { get; set; }
    }
}
