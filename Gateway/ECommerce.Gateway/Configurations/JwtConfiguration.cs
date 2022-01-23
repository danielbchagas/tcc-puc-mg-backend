using ECommerce.Compras.Gateway.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.Compras.Gateway.Configurations
{
    public static class JwtConfiguration
    {
        public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptionsSection = configuration.GetSection("JwtOptions");
            var jwtOptions = jwtOptionsSection.Get<JwtOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
                    ValidateIssuer = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtOptions.Audience
                };
            }).AddGoogle(options => 
            {
                options.ClientId = "949219568038-hdt285lngu3ajgaffa8b64ttosq1vver.apps.googleusercontent.com";
                options.ClientSecret = "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";
            });
        }
    }
}
