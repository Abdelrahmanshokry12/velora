using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace velora.core.Data
{
    public class Product : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Concern { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; } 
        public string PictureUrl { get; set; }

        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; } 

        public int ProductBrandId { get; set; }
        public ProductBrand ProductBrand { get; set; }

        public string SkinType { get; set; }
        public int SalesCount { get; set; } = 0;
        public bool IsBestSeller { get; set; } = false;

        public int StockQuantity { get; set; }

    }
}
