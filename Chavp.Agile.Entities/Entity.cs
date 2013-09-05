using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public abstract class Entity
    {
        public Entity()
        {
            Id = Guid.NewGuid();
            Created = DateTime.Now;
        }

        public virtual Guid Id { get; protected set; }
        public virtual DateTime Created { get; protected set; }
        public virtual DateTime Version { get; protected set; }
    }
}
