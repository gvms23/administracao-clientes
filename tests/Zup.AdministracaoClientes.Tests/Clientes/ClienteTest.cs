using System.Linq;
using FluentValidation;
using FluentValidation.Results;
using Xunit;
using Zup.AdministracaoClientes.Domain.Entities;
using Zup.AdministracaoClientes.Domain.Validations;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.Tests.Clientes
{
    public class ClienteTest
    {

        [Fact(DisplayName = "Criação do Cliente (Dados Válidos)")]
        public void CriacaoCliente_DadosValidos_RetornaVerdadeiro()
        {
            var _cliente = new Cliente("João", "363.348.820-07");

            _cliente.AdicionarEnderecos(
                                    new Endereco("Av. São Jorge",
                                        1074,
                                        "Cidade Salvador",
                                        "Jacareí",
                                        "SP",
                                        "Brasil",
                                        "12312000"));

            _cliente.AdicionarTelefones(
                new Telefone(12992319064));

            ValidationResult _result = new ClienteValidator().Validate(_cliente);

            Assert.True(_result.IsValid, string.Join(';', _result.Errors.Select(s => s.ErrorMessage)));
        }

        [Fact(DisplayName = "Criação do Cliente (CPF Inválido)")]
        public void CriacaoCliente_CPFInvalido_RetornaFalso()
        {
            var _cliente = new Cliente("João", "123.456.555-87");

            _cliente.AdicionarEnderecos(
                new Endereco("Av. São Jorge",
                    1074,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000"));

            _cliente.AdicionarTelefones(
                new Telefone(12992319064));

            ValidationResult _result = new ClienteValidator().Validate(_cliente);

            Assert.False(_result.IsValid, string.Join(';', _result.Errors.Select(s => s.ErrorMessage)));
        }

        [Fact(DisplayName = "Criação do Cliente (Nenhum Endereço)")]
        public void CriacaoCliente_NenhumEndereco_RetornaFalso()
        {
            var _cliente = new Cliente("João", "363.348.820-07");

            _cliente.AdicionarTelefones(
                new Telefone(12992319064));

            ValidationResult _result = new ClienteValidator().Validate(_cliente);

            Assert.False(_result.IsValid, string.Join(';', _result.Errors.Select(s => s.ErrorMessage)));
        }

        [Fact(DisplayName = "Criação do Cliente (Nenhum Telefone)")]
        public void CriacaoCliente_NenhumTelefone_RetornaFalso()
        {
            var _cliente = new Cliente("João", "363.348.820-07");

            _cliente.AdicionarEnderecos(
                new Endereco("Av. São Jorge",
                    1074,
                    "Cidade Salvador",
                    "Jacareí",
                    "SP",
                    "Brasil",
                    "12312000"));

            ValidationResult _result = new ClienteValidator().Validate(_cliente);

            Assert.False(_result.IsValid, string.Join(';', _result.Errors.Select(s => s.ErrorMessage)));
        }
    }
}
