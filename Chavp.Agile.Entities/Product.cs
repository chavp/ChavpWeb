using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chavp.Agile.Entities
{
    public class Product
        : Entity
    {
        public Product()
        {
            _features = new List<Feature>();
        }

        public Product(string codeName) : this()
        {
            CodeName = codeName;
        }

        public virtual string CodeName { get; set; }
        public virtual string Brand { get; set; }
        public virtual string Name { get; set; }
        public virtual string Slogan { get; set; }
        public virtual EProductStatus Status { get; set; }

        public virtual string StatusDisplay
        {
            get
            {
                return Status.ToString();
            }
        }

        List<Feature> _features;
        public virtual IList<Feature> Features 
        {
            get
            {
                return _features.AsReadOnly();
            }
            protected set
            {
                _features = new List<Feature>(value);
            }
        }

        public virtual void AddFeature(Feature feature)
        {
            _features.Add(feature);
        }
    }
}
