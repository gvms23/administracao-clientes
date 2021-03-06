﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Zup.AdministracaoClientes.Domain.Entities;

namespace Zup.AdministracaoClientes.Domain.Interfaces.Repositories
{
    public interface IClienteRepository: IRepository<Cliente>
    {
        Task<List<Cliente>> GetAsync();

        Task<Cliente> GetByIdAsync(Guid id);

        Task<bool> CPFJaEmUsoAsync(ulong cpfSemPontuacao);

        Task<bool> EmailJaEmUsoAsync(string email);

        Task<bool> ExisteComId(Guid id);
    }
}
