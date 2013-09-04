using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public class Feature
        : Entity
    {

        public Feature()
        {
            Products = new List<Product>();
        }

        public Feature(string codeName)
            : this()
        {
            CodeName = codeName;
        }

        public virtual string CodeName { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        /// <summary>
        /// Event, Action
        /// </summary>
        public virtual string Action { get; set; }

        /// <summary>
        /// Benefit
        /// </summary>
        public virtual string Result { get; set; }

        /// <summary>
        /// Actore, As A
        /// </summary>
        public virtual string Object { get; set; }


        public virtual IList<Product> Products { get; set; }
    }
}
