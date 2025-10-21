using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;

namespace SostavSD.Data.Configurations
{
    public class ChapterConfiguration: IEntityTypeConfiguration<Chapter>
    {
        void IEntityTypeConfiguration<Chapter>.Configure(EntityTypeBuilder<Chapter> builder)
        {
            builder.ToTable(nameof(Chapter))
                .HasKey(k => k.ChapterId);
            builder.Property(p => p.ChapterName);
            builder.Property(p => p.Country)
                .HasMaxLength(5);
        }
    }
}
