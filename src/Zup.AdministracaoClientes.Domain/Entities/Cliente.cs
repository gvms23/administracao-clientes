using System.Collections.Generic;
using Zup.AdministracaoClientes.Domain.Entities.Base;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public string Nome { get; set; }

        public CPF CPF { get; set; }

        public ICollection<Endereco> Enderecos { get; set; }

        public ICollection<Telefone> Telefones { get; set; }
    }
}
