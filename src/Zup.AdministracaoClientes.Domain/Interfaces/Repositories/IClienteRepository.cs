using Zup.AdministracaoClientes.Domain.Entities;

namespace Zup.AdministracaoClientes.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Cliente Cadastrar(Cliente cliente);
    }
}
