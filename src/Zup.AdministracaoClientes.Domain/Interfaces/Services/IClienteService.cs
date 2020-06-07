using Zup.AdministracaoClientes.Domain.Entities;

namespace Zup.AdministracaoClientes.Domain.Interfaces.Services
{
    public interface IClienteService
    {
        Cliente Cadastrar(Cliente cliente);
    }
}
