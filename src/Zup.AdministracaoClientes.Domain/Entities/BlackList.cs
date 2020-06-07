using Zup.AdministracaoClientes.Domain.Entities.Base;
using Zup.AdministracaoClientes.Domain.ValueObjects;

namespace Zup.AdministracaoClientes.Domain.Entities
{
    public class BlackList : EntityBase
    {
        public CPF CPF { get; set; }
    }
}
