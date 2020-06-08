using Microsoft.EntityFrameworkCore;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    [Owned]
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
            CEP = new CEP(cep);
        }

        public string Rua { get; protected set; }
        public short? Numero { get; protected set; }
        public string Bairro { get; protected set; }
        public string Cidade { get; protected set; }
        public string Estado { get; protected set; }
        public string Pais { get; protected set; }

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

        private bool IsValid() => !Empty && CEP.Valid;
    }
}
