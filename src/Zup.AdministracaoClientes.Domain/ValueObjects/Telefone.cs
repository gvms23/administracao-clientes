using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    public class Telefone : IValueObject
    {
        private readonly long? _value;

        public Telefone(long value)
        {
            _value = value;
        }

        public static implicit operator Telefone(long value)
        {
            return new Telefone(value);
        }

        public static bool operator ==(Telefone value1, Telefone value2)
        {
            return value1 != null 
                   && value2 != null 
                   && value1._value.Equals(value2._value);
        }

        public static bool operator !=(Telefone value1, Telefone value2)
        {
            return value1 != null 
                   && value2 != null
                   && !value1._value.Equals(value2._value);
        }

        public int Length => _value.ToString().Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool IsMobile => _value.ToString().Length == 9;

        public int? DDD
        {
            get
            {
                var stringValue = _value.ToString();

                if (stringValue.Length <= 8 || stringValue.Length == 9)
                    return null;

                return int.Parse(stringValue.Substring(0, 2));
            }
        }

        public string Formatado
        {
            get
            {
                var stringValue = _value.ToString();
                if (string.IsNullOrEmpty(stringValue)) return null;

                switch (stringValue.Length)
                {
                    case 8:
                        return Convert.ToUInt64(_value).ToString(@"0000-0000");
                    case 9:
                        return Convert.ToUInt64(_value).ToString(@"00000-0000");
                    case 10:
                        return Convert.ToUInt64(_value).ToString(@"(00) 0000-0000");
                    case 11:
                        return Convert.ToUInt64(_value).ToString(@"(00) 00000-0000");
                    default:
                        return null;
                }
            }
        }

        public bool Empty => _value.HasValue == false;

        private bool IsValid()
        {
            var stringValue = _value.ToString();

            if (string.IsNullOrEmpty(stringValue))
                return false;

            return stringValue.Length == 8   // Residencial
                || stringValue.Length == 9   // Celular
                || stringValue.Length == 10  // Residencial com DDD
                || stringValue.Length == 11; // Celular com DDD
        }

        public override string ToString()
        {
            return _value.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj != null && _value.Equals(((Telefone)obj)._value);
        }

        public bool Equals(Telefone other)
        {
            return _value == other._value;
        }

        public override int GetHashCode() => _value.GetHashCode();
    }
}
