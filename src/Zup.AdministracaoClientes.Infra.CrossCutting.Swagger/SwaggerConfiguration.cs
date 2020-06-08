using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace Zup.AdministracaoClientes.Infra.CrossCutting.Swagger
{
    /// <summary>
    /// Referências: https://github.com/domaindrivendev/Swashbuckle.AspNetCore
    /// </summary>
    public static class SwaggerConfiguration
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            return services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = $"API {nameof(AdministracaoClientes)}",
                        Version = "v1",
                        Description = "API REST desenvolvida com ASP .NET Core 3.1, Docker, DDD, TDD, IoC, Unit of Work, Repository Pattern, xUnit, Custom Middlewares, etc.",
                        Contact = new OpenApiContact
                        {
                            Name = "Gabriel Vicente",
                            Url = new Uri("http://linkedin.com/in/gvms23"),
                            Email = "gabrielvicente.m@gmail.com",
                        }
                    });


                // Set the comments path for the Swagger JSON and UI.
                var _xmlPath = Path.Combine("wwwroot", "api-docs.xml");

                options.IncludeXmlComments(_xmlPath);
            });
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
        {
            return app.UseSwagger()
                        .UseSwaggerUI(c =>
                        {
                            // Rota para acessar a documentação.
                            c.RoutePrefix = "docs";
                            c.SwaggerEndpoint("../swagger/v1/swagger.json", "Documentação API v1");
                        });
        }
    }
}
