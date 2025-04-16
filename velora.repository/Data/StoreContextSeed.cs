using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using velora.core.Data;

namespace velora.repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync (StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../velora.repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands)
                        await dbcontext.Set<ProductBrand>().AddAsync(Brand);
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.ProuductTypes.Any())
            {
                var TypeData = File.ReadAllText("../velora.repository/Data/DataSeed/types.json");
                var Types = JsonSerializer.Deserialize<List<ProductType>>(TypeData);
                if (Types?.Count > 0)
                {
                    foreach (var Type in Types)
                        await dbcontext.Set<ProductType>().AddAsync(Type);
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Products.Any())
            {
                var ProductData = File.ReadAllText("../velora.repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if (Products?.Count > 0)
                {
                    foreach (var product in Products)
                        await dbcontext.Set<Product>().AddAsync(product);
                    await dbcontext.SaveChangesAsync();
                }
            }
        }
    }
}
