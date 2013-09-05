using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chavp.Agile.Entities;

namespace Chavp.Agile.UseCases.Data
{
    public class ProductDto
    {
        public string CodeName { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Slogan { get; set; }
        public string StatusDisplay { get; set; }

        public DateTime Version { get; set; }
    }
}
