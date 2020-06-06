using System.Collections.Generic;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    public class Endereco : IValueObject
    {
        private Endereco() { }

        public Endereco(
            string rua, 
            short? numero,
            string bairro,
            string cidade, 
            string estado, 
            string pais, 
            string cep)
        {
            Rua = rua;
            Numero = numero;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Pais = pais;
            CEP = cep;
        }

        public string Rua { get; }
        public short? Numero { get; }
        public string Bairro { get; }
        public string Cidade { get; }
        public string Estado { get; }
        public string Pais { get; }
        public CEP CEP { get; }

        public bool Empty => string.IsNullOrEmpty(Rua)
                          && !Numero.HasValue
                          && string.IsNullOrEmpty(Bairro)
                          && string.IsNullOrEmpty(Cidade)
                          && string.IsNullOrEmpty(Estado)
                          && string.IsNullOrEmpty(Pais)
                          && !CEP.Empty;

        public override string ToString() => $"{Rua}, {(Numero.HasValue ? Numero.ToString() : "S/N")}, {Bairro}, {Cidade} - {Estado}, {Pais}, {CEP}";
        
        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        private bool IsValid()
        {
            return !Empty && CEP.Valid;
        }
    }
}
