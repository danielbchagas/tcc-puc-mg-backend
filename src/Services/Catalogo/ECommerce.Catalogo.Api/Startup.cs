using ECommerce.Catalogo.Api.Configurations;
using ECommerce.Catalogo.Infrastructure.Data;
using ECommerce.WebApi.Core.Extensions;
using ECommerce.WebApi.Core.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace ECommerce.Catalogo.Api
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
            services.AddEntityFrameworkConfiguration<ApplicationDbContext>(Configuration);
            services.AddHealthCheckConfiguration<ApplicationDbContext>(Configuration);
            services.AddOptionsConfiguration(Configuration);
            services.AddSwaggerConfiguration("v1", "ECommerce.Catalogo.Api", "TCC PUC Minas - Api de Catalogo do E-Commerce");
            #endregion

            services.AddDependencyInjectionConfiguration();
            
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

                app.UseSwaggerConfiguration("ECommerce.Catalogo.Api v1");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
