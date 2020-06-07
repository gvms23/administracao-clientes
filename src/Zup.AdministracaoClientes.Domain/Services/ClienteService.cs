using FluentValidation.Results;
using System;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Interfaces.Repositories;
using Zup.AdministracaoClientes.Domain.Interfaces.Services;
using Zup.AdministracaoClientes.Domain.Validations;
using Zup.AdministracaoClientes.Infra.CrossCutting.Helpers;

namespace Zup.AdministracaoClientes.Domain.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IBlacklistRepository _blacklistRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly ClienteValidator _clienteValidator;

        public ClienteService(
            IBlacklistRepository blacklistRepository,
            IClienteRepository clienteRepository)
        {
            _blacklistRepository = blacklistRepository;
            _clienteRepository = clienteRepository;

            _clienteValidator = new ClienteValidator();
        }

        public Cliente Cadastrar(Cliente cliente)
        {
            ValidationResult _clienteValidation = _clienteValidator.Validate(cliente);

            if (!_clienteValidation.IsValid)
                throw new Exception(_clienteValidation.GetValidationMessage());

            if (CPFEstaNaBlacklist(cliente.CPF.SemPontuacao))
                throw new Exception("CPF não permitido");

            cliente = _clienteRepository.Cadastrar(cliente);

            return cliente;
        }

        private bool CPFEstaNaBlacklist(ulong? cpf) =>
            cpf.HasValue && _blacklistRepository.Exists(cpf.Value);
    }
}
