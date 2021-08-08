using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Handlers.Commands;
using ECommerce.Catalogo.Domain.Application.Handlers.Notifications;
using ECommerce.Catalogo.Domain.Application.Handlers.Queries;
using ECommerce.Catalogo.Domain.Application.Notifications;
using ECommerce.Catalogo.Domain.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using ECommerce.Catalogo.Infrastructure.Repositories;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace ECommerce.Catalogo.Api.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            #region Injeção de dependência
            // Mediator
            services.AddMediatR(typeof(Startup));

            // Mediator - Comandos
            services.AddScoped<IRequestHandler<AtualizarProdutoCommand, ValidationResult>, AtualizarProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, ValidationResult>, AdicionarProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<SubtrairProdutoCommand, ValidationResult>, SubtrairProdutoCommandHandler>();

            // Mediator - Queries
            services.AddScoped<IRequestHandler<BuscarProdutoPorIdQuery, Produto>, BuscarProdutoPorIdQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosFiltradosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosFiltradosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosQuery, IEnumerable<Produto>>, BuscarProdutosQueryHandler>();

            // Mediator - Notificações
            services.AddScoped<INotificationHandler<ProdutoCommitNotification>, ProdutoCommitNotificationHandler>();

            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ILogEventoRepository, LogEventoRepository>();
            #endregion
        }
    }
}
