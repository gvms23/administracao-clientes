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

        [Fact(DisplayName = "Criação do instância (Dados Válidos)")]
        public void CriacaoInstancia_DadosValidos_RetornaVerdadeiro()
        {
            var _cliente = new Cliente("João", "363.348.820-07", "joao@gmail.com");

            _cliente.AdicionarEnderecos(
                                new Endereco("Av. São Jorge",
                                    50,
                                    "Cidade Salvador",
                                    "Jacareí",
                                    "SP",
                                    "Brasil",
                                    "12312000"));

            _cliente.AdicionarTelefones(
                                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.True(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Criação de Instância (CPF Inválido)")]
        public void CriacaoInstancia_CPFInvalido_RetornaFalso()
        {
            var _cliente = new Cliente("João", "123.456.555-87", "joao@gmail.com");

            _cliente.AdicionarEnderecos(
                                new Endereco("Av. São Jorge",
                                    50,
                                    "Cidade Salvador",
                                    "Jacareí",
                                    "SP",
                                    "Brasil",
                                    "12312000"));

            _cliente.AdicionarTelefones(
                                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Criação de Instância (E-mail Inválido)")]
        public void CriacaoInstancia_EmailInvalido_RetornaFalso()
        {
            var _cliente = new Cliente("João", "123.456.555-87", "l.com");

            _cliente.AdicionarEnderecos(
                new Endereco("Av. São Jorge",
                    50,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000"));

            _cliente.AdicionarTelefones(
                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Criação de Instância (Nenhum Endereço)")]
        public void CriacaoInstancia_NenhumEndereco_RetornaFalso()
        {
            var _cliente = new Cliente("João", "363.348.820-07", "joao@gmail.com");

            _cliente.AdicionarTelefones(
                                new Telefone(12985654585));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }

        [Fact(DisplayName = "Criação de Instância (Nenhum Telefone)")]
        public void CriacaoInstancia_NenhumTelefone_RetornaFalso()
        {
            var _cliente = new Cliente("João", "363.348.820-07", "joao@gmail.com");

            _cliente.AdicionarEnderecos(
                                new Endereco("Av. São Jorge",
                                    50,
                                    "Cidade Salvador",
                                    "Jacareí",
                                    "SP",
                                    "Brasil",
                                    "12312000"));

            ValidationResult _clienteValidation = _clienteValidator.Validate(_cliente);

            Assert.False(_clienteValidation.IsValid, _clienteValidation.GetValidationMessage());
        }
    }
}
