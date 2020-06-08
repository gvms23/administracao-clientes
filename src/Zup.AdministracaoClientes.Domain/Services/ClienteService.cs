using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Interfaces.Repositories;
using Zup.AdministracaoClientes.Domain.Interfaces.Services;
using Zup.AdministracaoClientes.Domain.Interfaces.UoW;
using Zup.AdministracaoClientes.Domain.Types;
using Zup.AdministracaoClientes.Domain.Validations;
using Zup.AdministracaoClientes.Infra.CrossCutting.ExceptionHandler.Extensions;
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
                throw new ApiException(StatusCodes.Status400BadRequest, _clienteValidation.GetValidationMessageAsList());

            if (await _clienteRepository.CPFJaEmUsoAsync(cliente.CPF.SemPontuacao))
                throw new ApiException(
                    StatusCodes.Status409Conflict,
                    "Já existe um cadastro com o CPF informado");

            if (await _clienteRepository.EmailJaEmUsoAsync(cliente.Email.Value))
                throw new ApiException(
                    StatusCodes.Status409Conflict,
                    "Já existe um cadastro com o e-mail informado");

            if (CPFConstaNaBlacklist(cliente.CPF.SemPontuacao))
                throw new ApiException(
                    StatusCodes.Status403Forbidden,
                    "CPF não permitido");

            cliente = _clienteRepository.Create(cliente);

            await _uow.CommitAsync();

            return cliente;
        }

        public async Task DeleteAsync(Guid id)
        {
            Cliente _cliente = await _clienteRepository.GetByIdAsync(id);

            if (_cliente == null)
                throw new ApiException(
                    StatusCodes.Status404NotFound,
                    "Nenhum cliente encontrado com o Id.");

            _clienteRepository.Delete(_cliente);
            await _uow.CommitAsync();
        }

        private bool CPFConstaNaBlacklist(ulong cpf) =>
            _cpfBlacklist.CPFs.Contains(cpf.ToString());
    }
}
