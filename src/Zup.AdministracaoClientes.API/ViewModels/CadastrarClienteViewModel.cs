using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Zup.AdministracaoClientes.API.ViewModels
{
    public class CadastrarClienteViewModel
    {
        public string Nome { get; set; }

        public string CPF { get; set; }

        public IEnumerable<EnderecoViewModel> Enderecos { get; } = new List<EnderecoViewModel>();

        public IEnumerable<long> Telefones { get; } = new List<long>();
    }
}
