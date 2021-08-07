using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using ECommerce.Catalogo.Api.Middlewares;
using ECommerce.Catalogo.Domain.Application.Commands;
using ECommerce.Catalogo.Domain.Application.Handlers.Commands;
using ECommerce.Catalogo.Domain.Application.Handlers.Notifications;
using ECommerce.Catalogo.Domain.Application.Handlers.Queries;
using ECommerce.Catalogo.Domain.Application.Notifications;
using ECommerce.Catalogo.Domain.Application.Queries;
using ECommerce.Catalogo.Domain.Interfaces.Repositories;
using ECommerce.Catalogo.Domain.Models;
using ECommerce.Catalogo.Infrastructure.Data;
using ECommerce.Catalogo.Infrastructure.Repositories;
using FluentValidation.Results;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace ECommerce.Catalogo.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", true, true);

            if (environment.IsDevelopment())
                builder.AddUserSecrets<Startup>();

            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Identidade
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "api_produtos";
                });
            #endregion

            #region KissLog
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped((context) =>
            {
                return Logger.Factory.Get();
            });

            services.AddLogging(logging =>
            {
                logging.AddKissLog();
            });
            #endregion

            #region Entity Framework
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration.GetConnectionString("CatalogoDB"), sqlServerOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount: 6, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                });
            });
            #endregion

            #region Injeção de dependência
            // Mediator
            services.AddMediatR(typeof(Startup));
            
            // Mediator - Comandos
            services.AddScoped<IRequestHandler<AtualizarProdutoCommand, ValidationResult>, AtualizarProdutoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarProdutoCommand, ValidationResult>, AdicionarProdutoCommandHandler>();
            
            // Mediator - Queries
            services.AddScoped<IRequestHandler<BuscarProdutoPorIdQuery, Produto>, BuscarProdutoPorIdQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosFiltradosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosFiltradosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarProdutosPaginadosQuery, IEnumerable<Produto>>, BuscarProdutosPaginadosQueryHandler>();

            // Mediator - Notificações
            services.AddScoped<INotificationHandler<ProdutoCommitNotification>, ProdutoCommitNotificationHandler>();

            // Repositórios
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            #endregion

            #region Healh Checks
            services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ProdutosDB"));
            });
            #endregion

            #region Configurações padrão
            services.AddControllers().AddJsonOptions(
                opt =>
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }
            );

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "ECommerce.Produtos.Api",
                    Version = "v1",
                    Description = "TCC PUC Minas - Api de Produtos do E-Commerce",
                    Contact = new OpenApiContact { Name = "Daniel Boasquevisque das Chagas", Email = "daniel.boasq@gmail.com" },
                    License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/mit") }
                });
            });
            #endregion
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Produtos.Api v1"));
                #endregion
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            #region KissLog
            // Meu middleware
            app.UseMiddleware<KissLogMiddleware>();

            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options);
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }

        #region Métodos auxiliares KissLog
        private void ConfigureKissLog(IOptionsBuilder options)
        {
            // optional KissLog configuration
            options.Options
                .AppendExceptionDetails((ex) =>
                {
                    StringBuilder sb = new StringBuilder();

                    if (ex is NullReferenceException nullRefException)
                    {
                        sb.AppendLine("Important: check for null references");
                    }

                    return sb.ToString();
                });

            // KissLog internal logs
            options.InternalLog = (message) =>
            {
                Debug.WriteLine(message);
            };

            // register logs output
            RegisterKissLogListeners(options);
        }

        private void RegisterKissLogListeners(IOptionsBuilder options)
        {
            // multiple listeners can be registered using options.Listeners.Add() method

            // register KissLog.net cloud listener
            options.Listeners.Add(new RequestLogsApiListener(new KissLog.CloudListeners.Auth.Application(
                Configuration["KissLog.OrganizationId"],    //  "69f89467-88be-439b-af23-79a80d466298"
                Configuration["KissLog.ApplicationId"])     //  "ec373522-8d35-4fc2-ae32-e5b3a5e99b3a"
            )
            {
                ApiUrl = Configuration["KissLog.ApiUrl"]    //  "https://api.kisslog.net"
            });
        }
        #endregion
    }
}
