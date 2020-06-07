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

        public Task<List<Cliente>> Get()
               => Query(wh => !wh.IsDeleted).ToListAsync();
    }
}
