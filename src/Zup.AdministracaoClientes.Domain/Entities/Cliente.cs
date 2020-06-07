using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Zup.AdministracaoClientes.Domain.Entities.Base;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.Domain.Entities
{
    public class Cliente : EntityBase
    {
        protected Cliente()
        {
            // EF
        }
        public Cliente(string nome, string cpf)
        {
            Nome = nome;
            CPF = new CPF(cpf);
        }

        public string Nome { get; private set; }

        public CPF CPF { get; private set; }

        public ICollection<Endereco> Enderecos { get; } = new List<Endereco>();

        public ICollection<Telefone> Telefones { get; } = new List<Telefone>();

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
