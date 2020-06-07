using System.Collections.Generic;
using Zup.AdministracaoClientes.Domain.Entities.Base;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.Domain.Entities
{
    public class Cliente : EntityBase
    {
        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            CPF = new CPF(cpf);

            Enderecos = new List<Endereco>();
            Telefones = new List<Telefone>();
        }

        public string Nome { get; set; }

        public CPF CPF { get; set; }

        public ICollection<Endereco> Enderecos { get; private set; }

        public ICollection<Telefone> Telefones { get; private set; }

        public void AdicionarEnderecos(params Endereco[] enderecos)
        {
            foreach (Endereco endereco in enderecos)
                Enderecos.Add(endereco);
        }

        public void AdicionarTelefones(params Telefone[] telefones)
        {
            foreach (Telefone telefone in telefones)
                Telefones.Add(telefone);
        }
    }
}
