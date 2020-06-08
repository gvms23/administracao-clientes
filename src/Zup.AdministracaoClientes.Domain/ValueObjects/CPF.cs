using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    [Owned]
    public class CPF : IValueObject
    {

        public CPF()
        {
            // EFCore.
            Value = Value?.Replace(".", string.Empty)
                          .Replace("-", string.Empty);
        }

        public CPF(string cpf)
        {
            Value = cpf?.Replace(".", string.Empty)
                        .Replace("-", string.Empty);
        }

        public string Value { get; protected set; }

        public string Formatado => string.IsNullOrEmpty(Value) || Invalid
            ? null
            : Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");

        public ulong SemPontuacao => Convert.ToUInt64(Value);

        public int Length => string.IsNullOrEmpty(Value) ? 0 : Value.Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool Empty => string.IsNullOrWhiteSpace(Value) || Value.Length == 0;

        private bool IsValid()
        {
            var _valueToValidate = Value;
            if (string.IsNullOrEmpty(_valueToValidate))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            _valueToValidate = _valueToValidate.Trim();
            _valueToValidate = _valueToValidate.Replace(".", "").Replace("-", "");
            if (_valueToValidate.Length != 11)
                return false;
            tempCpf = _valueToValidate.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
            {
                if (!int.TryParse(tempCpf[i].ToString(), out var result))
                    break;
                soma += result * multiplicador1[i];
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
            {
                if (!int.TryParse(tempCpf[i].ToString(), out var result))
                    break;

                soma += result * multiplicador2[i];
            }
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            return _valueToValidate.EndsWith(digito);
        }

        public override string ToString() => SemPontuacao.ToString();
    }
}
