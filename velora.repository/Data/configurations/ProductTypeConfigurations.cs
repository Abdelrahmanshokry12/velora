using Microsoft.EntityFrameworkCore;
using velora.core.Data;

namespace velora.repository.Data.configurations
{
    internal class ProductTypeConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
