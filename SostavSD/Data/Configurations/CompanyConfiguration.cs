using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("Company")
                .HasKey(k => k.CompanyID);
            builder.Property(p => p.CompanyName);
            builder.Property(p => p.CompanyDetails);                ;
            builder.Property(p => p.CompanyID);

        }
    }
}
