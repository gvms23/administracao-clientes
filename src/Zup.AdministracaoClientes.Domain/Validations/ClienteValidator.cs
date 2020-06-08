using System.Linq;
using FluentValidation;
using Zup.AdministracaoClientes.Domain.Entities;

namespace Zup.AdministracaoClientes.Domain.Validations
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c.Telefones)
                .Must(m => m.Any())
                .WithMessage("O cliente deve ter pelo menos 1 telefone");

            RuleFor(c => c.Enderecos)
                .Must(m => m.Any())
                .WithMessage("O cliente deve ter pelo menos 1 endereço");

            RuleFor(c => c.CPF)
                .Must(m => m.Valid)
                .WithMessage("O CPF fornecido é inválido");

            RuleFor(c => c.Email)
                .Must(m => m.Valid)
                .WithMessage("O e-mail fornecido é inválido");
        }
    }
}
