using System;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    public struct CEP : IValueObject
    {
        private readonly string _value;

        public CEP(string value)
        {
            _value = null;

            if (string.IsNullOrEmpty(value) || value.Length < 8)
                return;

            value = value.Replace("-", string.Empty);

            _value = value.Substring(0, 8);
        }

        public static implicit operator CEP(string value)
        {
            return new CEP(value);
        }

        public static bool operator ==(CEP value1, CEP value2)
        {
            return value1._value.Equals(value2._value);
        }

        public static bool operator !=(CEP value1, CEP value2)
        {
            return !(value1._value.Equals(value2._value));
        }

        public string Formatado => string.IsNullOrEmpty(_value)
                                            ? null
                                            : Convert.ToUInt64(_value).ToString(@"00000\-000");

        public int Length => string.IsNullOrEmpty(_value) ? 0 : _value.Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool Empty => string.IsNullOrWhiteSpace(_value) || _value.Length == 0;

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(_value))
                return false;

            switch (_value)
            {
                case "00000000":
                case "11111111":
                case "22222222":
                case "33333333":
                case "44444444":
                case "55555555":
                case "66666666":
                case "77777777":
                case "88888888":
                case "99999999":
                    return false;
                default:
                    return true;
            }
        }

        public override string ToString()
        {
            return _value;
        }

        public override bool Equals(object obj)
        {
            return obj != null && _value.Equals(((CEP)obj)._value);
        }

        public bool Equals(CEP other)
        {
            return string.Equals(_value, other._value);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value);
        }
    }
}
