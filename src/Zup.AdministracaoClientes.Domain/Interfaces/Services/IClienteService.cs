using System;
using System.Threading.Tasks;
using Zup.AdministracaoClientes.Domain.Entities;

namespace Zup.AdministracaoClientes.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Task<Cliente> Cadastrar(Cliente cliente);

        Task DeleteAsync(Guid id);
    }
}
