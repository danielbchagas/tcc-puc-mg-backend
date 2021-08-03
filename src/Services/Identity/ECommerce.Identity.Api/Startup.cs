using ECommerce.Identity.Api.Data;
using KissLog;
using KissLog.AspNetCore;
using KissLog.CloudListeners.RequestLogsListener;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Identity.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            #region Identidade
            services.AddDbContext<ApplicationDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDB"));
            });

            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            #endregion

            #region Injeção de dependência

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

            #region Healh Checks
            services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>();

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("IdentityDB"));
            });
            #endregion

            services.AddControllers();

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "ECommerce.Identity.Api", Version = "v1",
                    Description = "TCC PUC Minas - Api de Identidade do E-Commerce",
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

                #region
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity v1"));
                #endregion
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            #region KissLog
            app.UseKissLogMiddleware(options =>
            {
                ConfigureKissLog(options);
            });
            #endregion

            app.UseAuthentication();
            app.UseAuthorization();

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
