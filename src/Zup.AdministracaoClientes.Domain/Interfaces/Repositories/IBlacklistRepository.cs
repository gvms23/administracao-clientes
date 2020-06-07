namespace Zup.AdministracaoClientes.Domain.Interfaces.Repositories
{
    public interface IBlacklistRepository
    {
        bool Exists(ulong cpf);
    }
}
