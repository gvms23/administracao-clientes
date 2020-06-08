using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Zup.AdministracaoClientes.Data.Context;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Interfaces.Repositories;

namespace Zup.AdministracaoClientes.Data.Repositories
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AdministracaoClientesContext context)
            : base(context)
        {
        }

        public async Task<List<Cliente>> GetAsync()
            => await Query(wh => !wh.IsDeleted).ToListAsync();

        public async Task<Cliente> GetByIdAsync(Guid id)
            => await FindAsync(wh => !wh.IsDeleted && wh.Id == id);

        public async Task<bool> CPFJaEmUsoAsync(ulong cpfSemPontuacao) 
            => await Query(wh => wh.CPF.Value == cpfSemPontuacao.ToString()).AnyAsync();

        public async Task<bool> EmailJaEmUsoAsync(string email) 
            => await Query(wh => wh.Email.Value == email).AnyAsync();
    }
}
