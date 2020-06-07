using System;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    public struct CPF : IValueObject
    {
        private readonly string _value;

        public CPF(string value)
        {
            value = value?.Replace(".", string.Empty)
                .Replace(".", string.Empty)
                .Replace("-", string.Empty);

            _value = value;
        }

        public static implicit operator CPF(string value)
        {
            return new CPF(value);
        }

        public static bool operator ==(CPF value1, CPF value2)
        {
            return value1._value.Equals(value2._value);
        }

        public static bool operator !=(CPF value1, CPF value2)
        {
            return !(value1._value.Equals(value2._value));
        }

        public string Formatado
        {
            get
            {
                if (string.IsNullOrEmpty(_value) || Invalid)
                    return null;
                return Convert.ToUInt64(_value).ToString(@"000\.000\.000\-00");
            }
        }

        public ulong? SemPontuacao => Valid ? Convert.ToUInt64(_value) : (ulong?)null;

        public int Length => string.IsNullOrEmpty(_value) ? 0 : _value.Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool Empty => string.IsNullOrWhiteSpace(_value) || _value.Length == 0;

        private bool IsValid()
        {
            var _valueToValidate = _value;
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

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && _value.Equals(((CPF)obj)._value);
        }

        public bool Equals(CPF other)
        {
            return string.Equals(_value, other._value);
        }

        public override int GetHashCode() => _value != null ? _value.GetHashCode() : 0;
    }
}
