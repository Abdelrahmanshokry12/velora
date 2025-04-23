using Microsoft.EntityFrameworkCore;

namespace velora.core.Data.configurations
{
    internal class ProductTypeConfigurations : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductCategory> builder)
        {
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
