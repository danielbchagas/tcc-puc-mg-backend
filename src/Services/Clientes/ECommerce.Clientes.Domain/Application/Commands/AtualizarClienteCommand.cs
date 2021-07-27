using ECommerce.Clientes.Domain.Models;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace ECommerce.Clientes.Domain.Application.Commands
{
    public class AtualizarClienteCommand : IRequest<ValidationResult>
    {
        public AtualizarClienteCommand(Guid clienteId, string nomeFantasia, string cnpj, bool clienteAtivo, Guid enderecoId, string logradouro, string bairro, string cidade, string cep, Estados estado, bool enderecoAtivo)
        {
            ClienteId = clienteId;
            NomeFantasia = nomeFantasia;
            Cnpj = cnpj;
            ClienteAtivo = clienteAtivo;

            EnderecoId = enderecoId;
            Logradouro = logradouro;
            Bairro = bairro;
            Cidade = cidade;
            Cep = cep;
            Estados = estado;
            EnderecoAtivo = enderecoAtivo;
        }

        // Log do evento
        public string OrigemRequisicao { get; private set; }
        public string Uri { get; private set; }

        // Cliente
        public Guid ClienteId { get; private set; }
        public string NomeFantasia { get; private set; }
        public string Cnpj { get; private set; }
        public bool ClienteAtivo { get; private set; }

        // Endereco
        public Guid EnderecoId { get; private set; }
        public string Logradouro { get; private set; }
        public string Bairro { get; private set; }
        public string Cidade { get; private set; }
        public string Cep { get; private set; }
        public Estados Estados { get; private set; }
        public bool EnderecoAtivo { get; private set; }

        // Métodos auxiliares
        public void AdicionarOrigemRequisicao(string origemRequisicao)
        {
            OrigemRequisicao = origemRequisicao;
        }

        public void AdicionarUri(string uri)
        {
            Uri = uri;
        }
    }

    public class AtualizarClienteCommandValidation : AbstractValidator<AtualizarClienteCommand>
    {
        public AtualizarClienteCommandValidation()
        {
            RuleFor(_ => _.ClienteId).NotEqual(Guid.Empty).WithMessage("{PropertyName} Inválido!");
            RuleFor(_ => _.NomeFantasia)
                .MaximumLength(100).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Cnpj)
                .MaximumLength(18).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");

            RuleFor(_ => _.EnderecoId).NotEqual(Guid.Empty).WithMessage("{PropertyName} Inválido!");
            RuleFor(_ => _.Logradouro)
                .MaximumLength(200).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Bairro)
                .MaximumLength(50).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Cidade)
                .MaximumLength(50).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
            RuleFor(_ => _.Cep)
                .MaximumLength(9).WithMessage("{PropertyName} excedeu o tamanho máximo!")
                .NotNull().NotEmpty().WithMessage("{PropertyName} inválido!");
        }
    }
}
