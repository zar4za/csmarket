using CsMarket.Auth;
using CsMarket.Auth.Jwt;
using CsMarket.Core;
using CsMarket.Steam;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
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
            services.AddTransient<IChallengeProvider, SteamOpenIdProvider>();

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

        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            var config = new TypeAdapterConfig();

            TypeAdapterConfig<Data.Entities.Listing, Market.Listing>
                .NewConfig()
                .Map(dest => dest.ListingId, src => src.Id)
                .Map(dest => dest.Price, src => src.Price)
                .Map(dest => dest.AssetId, src => src.Asset.AssetId)
                .Map(dest => dest.IconHash, src => src.Asset.ClassName.IconUrl)
                .Map(dest => dest.MarketHashName, src => src.Asset.ClassName.MarketHashName);

            TypeAdapterConfig<Data.Entities.Asset, Item>
                .NewConfig()
                .Map(dest => dest.AssetId, src => src.AssetId)
                .Map(dest => dest.ClassId, src => src.ClassName.ClassId)
                .Map(dest => dest.IconUrl, src => src.ClassName.IconUrl)
                .Map(dest => dest.MarketHashName, src => src.ClassName.MarketHashName);

            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
