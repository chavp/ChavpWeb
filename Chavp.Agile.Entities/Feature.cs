using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    /// <summary>
    /// http://en.wikipedia.org/wiki/Feature-driven_development
    /// </summary>
    public class Feature
        : Entity
    {

        protected Feature()
        {
            Products = new List<Product>();
        }

        public Feature(string codeName)
            : this()
        {
            CodeName = codeName;
        }

        public virtual string CodeName { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        /// <summary>
        /// 'Calculate the total of a sale' or 'Validate the password of a user'
        /// Action are Calculate and Validate
        /// </summary>
        public virtual string Action { get; set; }

        /// <summary>
        /// 'Calculate the total of a sale' or 'Validate the password of a user'
        /// Result are the total, the password
        /// </summary>
        public virtual string Result { get; set; }

        /// <summary>
        /// 'Calculate the total of a sale' or 'Validate the password of a user'
        /// Object are sale, user
        /// </summary>
        public virtual string Object { get; set; }


        public virtual IList<Product> Products { get; set; }
    }
}
