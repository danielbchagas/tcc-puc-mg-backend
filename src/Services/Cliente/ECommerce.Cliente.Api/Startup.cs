using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using ECommerce.Cliente.Api.Middlewares;
using ECommerce.Cliente.Domain.Application.Commands;
using ECommerce.Cliente.Domain.Application.Handlers.Commands;
using ECommerce.Cliente.Domain.Application.Handlers.Notifications;
using ECommerce.Cliente.Domain.Application.Handlers.Queries;
using ECommerce.Cliente.Domain.Application.Notifications;
using ECommerce.Cliente.Domain.Application.Queries;
using ECommerce.Cliente.Domain.Interfaces.Repositories;
using ECommerce.Cliente.Domain.Models;
using ECommerce.Cliente.Infrastructure.Data;
using ECommerce.Cliente.Infrastructure.Repositories;
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

namespace ECommerce.Cliente.Api
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Identidade
            services.AddAuthentication("Bearer")
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.Audience = "api_clientes";
                });
            #endregion

            #region Entity Framework
            services.AddDbContext<ApplicationDbContext>(optionsAction =>
            {
                optionsAction.UseSqlServer(Configuration.GetConnectionString("ClientesDB"), sqlServerOptionsAction: options =>
                {
                    options.EnableRetryOnFailure(maxRetryCount: 6, maxRetryDelay: TimeSpan.FromSeconds(10), errorNumbersToAdd: null);
                });
            });
            #endregion

            #region Injeção de dependência
            services.AddMediatR(typeof(Startup));

            #region Mediator - Comandos
            services.AddScoped<IRequestHandler<AtualizarClienteCommand, ValidationResult>, AtualizarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarClienteCommand, ValidationResult>, AdicionarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<DesativarClienteCommand, ValidationResult>, DesativarClienteCommandHandler>();

            services.AddScoped<IRequestHandler<AtualizarEnderecoCommand, ValidationResult>, AtualizarEnderecoCommandHandler>();
            services.AddScoped<IRequestHandler<AdicionarEnderecoCommand, ValidationResult>, AdicionarEnderecoCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarDocumentoCommand, ValidationResult>, AdicionarDocumentoCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarDocumentoCommand, ValidationResult>, AtualizarDocumentoCommandHandler>();

            services.AddScoped<IRequestHandler<AdicionarEmailCommand, ValidationResult>, AdicionarEmailCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarEmailCommand, ValidationResult>, AtualizarEmailCommandHandler>();
            #endregion

            #region Mediator - Queries
            services.AddScoped<IRequestHandler<BuscarClientePorIdQuery, Domain.Models.Cliente>, BuscarClientesPorIdQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarClientesFiltradosPaginadosQuery, IEnumerable<Domain.Models.Cliente>>, BuscarClienteFiltradosPaginadosQueryHandler>();
            services.AddScoped<IRequestHandler<BuscarClientesPaginadosQuery, IEnumerable<Domain.Models.Cliente>>, BuscarClientesPaginadosQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarEnderecoPorIdQuery, Endereco>, BuscarEnderecoPorIdQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarEmailPorIdQuery, Email>, BuscarEmailPorIdQueryHandler>();

            services.AddScoped<IRequestHandler<BuscarDocumentoPorIdQuery, Documento>, BuscarDocumentoPorIdQueryHandler>();
            #endregion

            #region Mediator - Notificações
            services.AddScoped<INotificationHandler<ClienteCommitNotification>, ClienteCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<EnderecoCommitNotification>, EnderecoCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<DocumentoCommitNotification>, DocumentoCommitNotificationHandler>();

            services.AddScoped<INotificationHandler<EmailCommitNotification>, EmailCommitNotificationHandler>();
            #endregion

            #region Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<ILogEventoRepository, LogEventoRepository>();
            services.AddScoped<IDocumentoRepository, DocumentoRepository>();
            services.AddScoped<IEmailRepository, EmailRepository>();
            #endregion
            #endregion

            #region Healh Checks
            services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("ClientesDB"));
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
                services.AddSwaggerGen(c =>
                {
                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo
                        {
                            Title = "ECommerce.Cliente.Api",
                            Version = "v1",
                            Description = "TCC PUC Minas - Api de Cliente do E-Commerce",
                            Contact = new OpenApiContact { Name = "Daniel Boasquevisque das Chagas", Email = "daniel.boasq@gmail.com" },
                            License = new OpenApiLicense { Name = "MIT", Url = new Uri("https://opensource.org/licenses/mit") }
                        });
                    });
                });
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                #region Swagger
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Cliente.Api v1"));
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
