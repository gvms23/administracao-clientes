using System.Collections.Generic;

namespace Zup.AdministracaoClientes.API.ViewModels
{
    public class CadastrarClienteViewModel
    {
        public CadastrarClienteViewModel() { }
        public CadastrarClienteViewModel(
            string nome,
            string email,
            string cpf,
            IEnumerable<EnderecoViewModel> enderecos,
            IEnumerable<long> telefones)
        {
            Nome = nome;
            Email = email;
            CPF = cpf;
            Enderecos = enderecos ?? new List<EnderecoViewModel>();
            Telefones = telefones ?? new List<long>();
        }

        public string Nome { get; protected set; }

        public string Email { get; protected set; }

        public string CPF { get; protected set; }

        public IEnumerable<EnderecoViewModel> Enderecos { get; set; } = new List<EnderecoViewModel>();

        public IEnumerable<long> Telefones { get; set; } = new List<long>();
    }
}
