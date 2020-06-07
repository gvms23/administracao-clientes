using FluentValidation.Results;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Interfaces.Repositories;
using Zup.AdministracaoClientes.Domain.Interfaces.Services;
using Zup.AdministracaoClientes.Domain.Interfaces.UoW;
using Zup.AdministracaoClientes.Domain.Types;
using Zup.AdministracaoClientes.Domain.Validations;
using Zup.AdministracaoClientes.Infra.CrossCutting.Helpers;

namespace Zup.AdministracaoClientes.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IUnitOfWork _uow;
        private readonly ClienteValidator _clienteValidator;
        private readonly CPFBlacklistType _cpfBlacklist;

        public ClienteService(
            IClienteRepository clienteRepository,
            IUnitOfWork uow,
            IOptions<CPFBlacklistType> _optionsBlacklist)
        {
            _clienteRepository = clienteRepository;
            _uow = uow;
            _cpfBlacklist = _optionsBlacklist.Value;

            _clienteValidator = new ClienteValidator();
        }

        public async Task<Cliente> Cadastrar(Cliente cliente)
        {
            ValidationResult _clienteValidation = _clienteValidator.Validate(cliente);

            if (!_clienteValidation.IsValid)
                throw new Exception(_clienteValidation.GetValidationMessage());

            if (CPFEstaNaBlacklist(cliente.CPF.SemPontuacao))
                throw new Exception("CPF não permitido");

            cliente = _clienteRepository.Create(cliente);

            await _uow.CommitAsync();

            return cliente;
        }

        private bool CPFEstaNaBlacklist(ulong? cpf) =>
            cpf.HasValue && _cpfBlacklist.CPFs.Contains(cpf.Value.ToString());
    }
}
