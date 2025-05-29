using Microsoft.OpenApi.Models;

namespace FCG.API.Configuration.Swagger;

public static class SwaggerExtensions
{
    public static void AddSwaggerDocumentation(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "FIAP Cloud Game API",
                Version = "v1",
                Description = "A FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais e gestão de servidores para partidas online."
            });

            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please, insert 'Bearer' [space] and the token JWT",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            c.SchemaFilter<EnumSchemaFilter>();
        });
    }
}