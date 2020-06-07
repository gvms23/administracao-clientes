using Microsoft.Extensions.DependencyInjection;
using System;
using Zup.AdministracaoClientes.Data.Repositories;
using Zup.AdministracaoClientes.Data.UoW;
using Zup.AdministracaoClientes.Domain.Interfaces.Repositories;
using Zup.AdministracaoClientes.Domain.Interfaces.Services;
using Zup.AdministracaoClientes.Domain.Interfaces.UoW;
using Zup.AdministracaoClientes.Domain.Services;

namespace Zup.AdministracaoClientes.Infra.CrossCutting.IoC
{
    public static class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // UoW
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Repositories
            services.AddScoped<IClienteRepository, ClienteRepository>();

            // Domain Services
            services.AddScoped<IClienteService, ClienteService>();
        }
    }
}
