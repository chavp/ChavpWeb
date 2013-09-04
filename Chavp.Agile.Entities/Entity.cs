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
            Created = DateTime.Now;
        }

        public virtual DateTime Created { get; protected set; }
        public virtual DateTime Version { get; protected set; }
    }
}
