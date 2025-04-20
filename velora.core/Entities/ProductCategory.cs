using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace velora.core.Data
{
    public class ProductCategory : BaseEntity<int> 
    {
        public string Name { get; set; }
    }
}
