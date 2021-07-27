using ECommerce.Clientes.Api.Middlewares;
using ECommerce.Clientes.Domain.Application.Commands.Cliente;
using ECommerce.Clientes.Domain.Application.Handlers.Commands.Cliente;
using ECommerce.Clientes.Domain.Application.Handlers.Notifications;
using ECommerce.Clientes.Domain.Application.Notifications;
using ECommerce.Clientes.Domain.Application.Queries;
using ECommerce.Clientes.Domain.Interfaces.Queries;
using ECommerce.Clientes.Domain.Interfaces.Repositories;
using ECommerce.Clientes.Infrastructure.Data;
using ECommerce.Clientes.Infrastructure.Repositories;
using FluentValidation.Results;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

namespace ECommerce.Clientes.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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
            // Mediator
            services.AddMediatR(typeof(Startup));
            // Mediator - Comandos
            services.AddScoped<IRequestHandler<AtualizarClienteCommand, ValidationResult>, AtualizarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<CadastrarClienteCommand, ValidationResult>, CadastrarClienteCommandHandler>();
            services.AddScoped<IRequestHandler<DesativarClienteCommand, ValidationResult>, DesativarClienteCommandHandler>();
            // Mediator - Queries
            services.AddScoped<IBuscarClientePorIdQuery, BuscarClientePorIdQuery>();
            services.AddScoped<IBuscarClientesFiltradosPaginadosQuery, BuscarClientesFiltradosPaginadosQuery>();
            services.AddScoped<IBuscarClientesPaginadosQuery, BuscarClientesPaginadosQuery>();
            // Mediator - Notificações
            services.AddScoped<INotificationHandler<ClienteCommitNotification>, ClienteCommitNotificationHandler>();

            // Repositórios
            services.AddScoped<IClienteRepository, ClienteRepository>();
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

            services.AddControllers().AddJsonOptions(
                opt => 
                {
                    opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                }
            );

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ECommerce.Clientes.Api", Version = "v1" });
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ECommerce.Clientes.Api v1"));
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
