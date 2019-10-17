using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Domain
{
    public abstract class Entity
    {
        public DateTime? DeletedDate { get; set; }

        public DateTime CreationDate { get; set; } = DateTime.Now;

        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
