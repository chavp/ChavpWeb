using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public abstract class Entity
    {
        public virtual DateTime Version { get; protected set; }
    }
}
