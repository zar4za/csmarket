using CsMarket.Models.Authentication;
using CsMarket.Services.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CsMarket.Infrastructure
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager configuration)
        {
            var settings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), settings);

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration[settings.Issuer],
                    ValidAudience = configuration[settings.Audience],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.Secret))
                };
            });

            return services;
        }
    }
}
