using ECommerce.Identity.Api.Configurations;
using ECommerce.Identity.Api.Services.gRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using ECommerce.Identity.Api.Handler;

namespace ECommerce.Identity.Api
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
            services.AddOptionsConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddIdentityConfiguration(Configuration);
            services.AddCorsConfiguration();
            services.AddGrpcConfiguration(Configuration);
            services.AddRabbitMqConfiguration(Configuration);
            services.AddAutoMapper(typeof(Startup));
            services.AddRefitConfiguration(Configuration);

            services.AddControllers();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<JwtHandler>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwaggerConfiguration();

                app.UseCors("development");
            }
            else
            {
                app.UseSwaggerConfiguration();

                app.UseCors("production");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseGrpcWeb();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<CustomerGrpcService>().EnableGrpcWeb();
            });
        }
    }
}
