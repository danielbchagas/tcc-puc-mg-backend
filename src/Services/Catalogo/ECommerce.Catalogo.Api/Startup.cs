using ECommerce.Catalogo.Api.Configurations;
using ECommerce.Catalogo.Api.Middlewares;
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
            services.AddKissLogConfiguration();
            services.AddSwaggerAuthenticationConfiguration();
            #endregion

            services.AddDependencyInjectionConfiguration();
            services.AddSwaggerConfiguration();

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

                app.UseSwaggerConfiguration();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseMiddleware<KissLogMiddleware>();
            app.UseKissLogConfiguration(Configuration);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
