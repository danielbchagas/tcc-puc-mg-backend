using EasyNetQ;
using ECommerce.Identity.Api.Configurations;
using ECommerce.Identity.Api.Data;
using ECommerce.Identity.Api.Models;
using ECommerce.WebApi.Core.Extensions;
using ECommerce.WebApi.Core.Helpers;
using ECommerce.WebApi.Core.Middlewares;
using ECommerce.WebApi.Core.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace ECommerce.Identity.Api
{
    public class Startup
    {
        public Startup(IWebHostEnvironment environment)
        {
            Configuration = new ConfigurationBuilderHelper(environment).Build<Startup>();
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            #region Core
            services.AddJwtConfiguration(Configuration);
            services.AddKissLogConfiguration();
            services.AddHealthCheckConfiguration<ApplicationDbContext>(Configuration);
            services.AddSwaggerConfiguration("v1", "ECommerce.Identidade.Api", "TCC PUC Minas - Api de Identidade do E-Commerce");
            services.AddOptionsConfiguration(Configuration);
            #endregion

            services.AddIdentityConfiguration(Configuration);
            
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
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerConfiguration("ECommerce.Identidade.Api v1");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<CustomExceptionHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
