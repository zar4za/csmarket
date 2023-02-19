using CsMarket.Auth;
using CsMarket.Auth.Jwt;
using CsMarket.Steam;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace CsMarket.Infrastructure
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddJwt(this IServiceCollection services, ConfigurationManager configuration)
        {
            var settings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), settings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = settings.Issuer,
                    ValidAudience = settings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(settings.Secret))
                };
            });


            services.AddSingleton<IJwtTokenGenerator>(x => new JwtTokenGenerator(settings));

            return services;
        }

        public static IServiceCollection AddSteam(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.Configure<SteamIdProviderOptions>(configuration);
            services.AddTransient<IChallengeProvider, SteamIdProvider>();

            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string> ()
                    }
                });
            });

            return services;
        }
    }
}
