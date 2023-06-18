using ECommerce.Customer.Api.Configurations;
using ECommerce.Customer.Api.Middlewares;
using ECommerce.Customer.Api.Services.gRPC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ECommerce.Customer.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

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

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityConfiguration(Configuration);
            services.AddEntityFrameworkConfiguration(Configuration);
            services.AddSwaggerConfiguration();
            services.AddOptionsConfiguration(Configuration);
            services.AddDependencyInjectionConfiguration();
            services.AddCorsConfiguration();
            services.AddAutoMapper(typeof(Startup));
            services.AddRefitConfiguration();

            services.AddControllers();

            services.AddGrpc();

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

            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGrpcService<CustomerGrpcService>().EnableGrpcWeb();
            });
        }
    }
}
