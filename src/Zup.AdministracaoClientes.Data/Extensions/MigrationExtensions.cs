using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Design;
using System;
using Microsoft.Extensions.Hosting;

namespace Zup.AdministracaoClientes.Data.Extensions
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/april/data-points-ef-core-in-a-docker-containerized-app
    /// </summary>
    public static class MigrationExtensions
    {
        public static IHost MigrateDatabase<T>(this IHost host) where T : DbContext
        {
            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider _services = scope.ServiceProvider;
                ILogger<Program> _logger = _services.GetRequiredService<ILogger<Program>>();
                try
                {
                    T context = _services.GetService<T>();

                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Um erro ocorreu enquanto o banco de dados era criado.");
                    throw;
                }
            }

            return host;
        }
    }
}
