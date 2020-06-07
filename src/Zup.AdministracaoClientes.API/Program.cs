using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.IO;
using Zup.AdministracaoClientes.Data.Context;
using Zup.AdministracaoClientes.Data.Extensions;

namespace Zup.AdministracaoClientes.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<AdministracaoClientesContext>()
                .Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .UseContentRoot(Directory.GetCurrentDirectory())
                        .UseSetting("https_port", "443")
                        .UseIISIntegration()
                        .UseStartup<Startup>();
                });
    }
}
