using System.Threading.Tasks;
using Zup.AdministracaoClientes.Data.Context;
using Zup.AdministracaoClientes.Domain.Interfaces.UoW;

namespace Zup.AdministracaoClientes.Data.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AdministracaoClientesContext _context;

        public UnitOfWork(AdministracaoClientesContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            int _changesNumber = await _context.SaveChangesAsync();
            return _changesNumber > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
