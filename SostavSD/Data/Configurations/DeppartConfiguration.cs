using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SostavSD.Entities;
using SostavSD.Models;

namespace SostavSD.Data.Configurations
{
    public class DeppartConfiguration: IEntityTypeConfiguration<Deppart>
    {
        void IEntityTypeConfiguration<Deppart>.Configure(EntityTypeBuilder<Deppart> builder)
        {
            builder.ToTable(nameof(Deppart))
                .HasKey(k => k.GroupId);
            builder.Property(p => p.GroupName)
                .HasMaxLength(25);
            builder.Property(p => p.GroupANU)
                .HasMaxLength(10);
        }
    }
}
