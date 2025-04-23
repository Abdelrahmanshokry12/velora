using Microsoft.Extensions.Logging;
using System.Data;
using System.Text.Json;
using velora.core.Data;
using velora.core.Data.Contexts;

namespace velora.repository
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext dbContext, ILoggerFactory loggerFactory)
        {
            var logger = loggerFactory.CreateLogger<StoreContextSeed>();
            bool hasChanges = false;

            try
            {
                if (!dbContext.ProductBrands.Any())
                {
                    var brandsData = await File.ReadAllTextAsync("../velora.repository/SeedData/brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    if (brands?.Count > 0)
                    {
                        await dbContext.ProductBrands.AddRangeAsync(brands);
                        hasChanges = true;
                        logger.LogInformation("Seeded product brands.");
                    }
                }

                if (!dbContext.ProductCategories.Any())
                {
                    var categoriesData = await File.ReadAllTextAsync("../velora.repository/SeedData/categories.json");
                    var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);
                    if (categories?.Count > 0)
                    {
                        await dbContext.ProductCategories.AddRangeAsync(categories);
                        hasChanges = true;
                        logger.LogInformation("Seeded product categories.");
                    }
                }

                if (!dbContext.Products.Any())
                {
                    var productsData = await File.ReadAllTextAsync("../velora.repository/SeedData/products.json");
                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                    if (products?.Count > 0)
                    {
                        await dbContext.Products.AddRangeAsync(products);
                        hasChanges = true;
                        logger.LogInformation("Seeded products.");
                    }
                }

                if (hasChanges)
                {
                    await dbContext.SaveChangesAsync();
                    logger.LogInformation("All seed data saved to database.");
                }
                else
                {
                    logger.LogInformation("No seed data changes detected. Skipping save.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred during seeding.");
            }
        }
    }
}


