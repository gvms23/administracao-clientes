using FluentValidation.Results;
using Xunit;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Validations;
using Zup.AdministracaoClientes.Domain.ValueObjects;
using Zup.AdministracaoClientes.Infra.CrossCutting.Helpers;

namespace Zup.AdministracaoClientes.Tests.Clientes
{
    public class ClienteTest
    {
        private readonly ClienteValidator _clienteValidator;
        public ClienteTest()
        {
            _clienteValidator = new ClienteValidator();
        }

        [Fact(DisplayName = "Cria��o do Cliente (Dados V�lidos)")]
        public void CriacaoCliente_DadosValidos_RetornaVerdadeiro()
        {
            var _cliente = new Cliente("Jo�o", "363.348.820-07");

            _cliente.AdicionarEnderecos(
                                new Endereco("Av. S�o Jorge",
                                    50,
                                    "Cidade Salvador",
                                    "Jacare�",
                                    "SP",
                                    "Brasil",
                                    "12312000"));

            _cliente.AdicionarTelefones(
                                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.True(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Cria��o do Cliente (CPF Inv�lido)")]
        public void CriacaoCliente_CPFInvalido_RetornaFalso()
        {
            var _cliente = new Cliente("Jo�o", "123.456.555-87");

            _cliente.AdicionarEnderecos(
                                new Endereco("Av. S�o Jorge",
                                    50,
                                    "Cidade Salvador",
                                    "Jacare�",
                                    "SP",
                                    "Brasil",
                                    "12312000"));

            _cliente.AdicionarTelefones(
                                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Cria��o do Cliente (Nenhum Endere�o)")]
        public void CriacaoCliente_NenhumEndereco_RetornaFalso()
        {
            var _cliente = new Cliente("Jo�o", "363.348.820-07");

            _cliente.AdicionarTelefones(
                                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Cria��o do Cliente (Nenhum Telefone)")]
        public void CriacaoCliente_NenhumTelefone_RetornaFalso()
        {
            var _cliente = new Cliente("Jo�o", "363.348.820-07");

            _cliente.AdicionarEnderecos(
                                new Endereco("Av. S�o Jorge",
                                    50,
                                    "Cidade Salvador",
                                    "Jacare�",
                                    "SP",
                                    "Brasil",
                                    "12312000"));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }
    }
}
