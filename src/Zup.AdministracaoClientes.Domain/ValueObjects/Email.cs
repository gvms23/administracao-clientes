using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.Mail;
using Zup.AdministracaoClientes.Domain.ValueObjects.Base;

namespace Zup.AdministracaoClientes.Domain.ValueObjects
{
    [Owned]
    public class Email : IValueObject
    {
        protected Email() { }

        public Email(string email)
        {
            Value = email;
        }

        public string Value { get; protected set; }

        public int Length => string.IsNullOrEmpty(Value) ? 0 : Value.Length;

        public bool Valid => IsValid();

        public bool Invalid => !IsValid();

        public bool Empty => string.IsNullOrEmpty(Value);

        private bool IsValid()
        {
            try
            {
                MailAddress _ = new MailAddress(Value);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public override string ToString() => Value;
    }
}
