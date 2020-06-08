using System;
using Microsoft.EntityFrameworkCore;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    [Owned]
    public class CEP : IValueObject
    {

        protected CEP() { }

        public CEP(string value)
        {
            Value = null;

            if (string.IsNullOrEmpty(value) || value.Length < 8)
                return;

            value = value.Replace("-", string.Empty);

            Value = value.Substring(0, 8);
        }

        public string Value { get; protected set; }

        public string Formatado => string.IsNullOrEmpty(Value)
                                            ? null
                                            : Convert.ToUInt64(Value).ToString(@"00000\-000");

        public int Length => string.IsNullOrEmpty(Value) ? 0 : Value.Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool Empty => string.IsNullOrWhiteSpace(Value) || Value.Length == 0;

        public override string ToString() => Value;


        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Value))
                return false;

            switch (Value)
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
    }
}
