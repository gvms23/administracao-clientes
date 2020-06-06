using System;

namespace Zup.AdministracaoClientes.Domain.Entities.Base
{
    public class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
