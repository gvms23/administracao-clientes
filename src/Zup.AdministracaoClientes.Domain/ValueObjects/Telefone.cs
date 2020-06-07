using System;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    public class Telefone : IValueObject
    {
        public long Value { get; private set; }

        protected Telefone()
        {
            // EF Core.
        }

        public Telefone(long value)
        {
            Value = value;
        }

        public static implicit operator Telefone(long value)
        {
            return new Telefone(value);
        }

        public int Length => Value.ToString().Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool IsMobile => Value.ToString().Length == 9 || Value.ToString().Length == 11;

        public int? DDD
        {
            get
            {
                var stringValue = Value.ToString();

                if (stringValue.Length <= 8 || stringValue.Length == 9)
                    return null;

                return int.Parse(stringValue.Substring(0, 2));
            }
        }

        public string Formatado
        {
            get
            {
                var stringValue = Value.ToString();
                if (string.IsNullOrEmpty(stringValue)) return null;

                switch (stringValue.Length)
                {
                    case 8:
                        return Convert.ToUInt64(Value).ToString(@"0000-0000");
                    case 9:
                        return Convert.ToUInt64(Value).ToString(@"00000-0000");
                    case 10:
                        return Convert.ToUInt64(Value).ToString(@"(00) 0000-0000");
                    case 11:
                        return Convert.ToUInt64(Value).ToString(@"(00) 00000-0000");
                    default:
                        return null;
                }
            }
        }

        public bool Empty => Value == default;

        private bool IsValid()
        {
            var stringValue = Value.ToString();

            if (string.IsNullOrEmpty(stringValue))
                return false;

            return stringValue.Length == 8   // Residencial
                || stringValue.Length == 9   // Celular
                || stringValue.Length == 10  // Residencial com DDD
                || stringValue.Length == 11; // Celular com DDD
        }

        public override string ToString() => Value == default ? null : Value.ToString();
    }
}
