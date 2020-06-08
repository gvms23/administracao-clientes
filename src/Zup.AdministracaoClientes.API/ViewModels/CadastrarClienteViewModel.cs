using System.Collections.Generic;

namespace Zup.AdministracaoClientes.API.ViewModels
{
    public class CadastrarClienteViewModel
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string CPF { get; set; }

        public IEnumerable<EnderecoViewModel> Enderecos { get; set; } = new List<EnderecoViewModel>();

        public IEnumerable<long> Telefones { get; set; } = new List<long>();
    }
}
