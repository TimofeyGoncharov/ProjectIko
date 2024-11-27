using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectIko.Models;

namespace ProjectIko.Db.Configuration
{
    public class ModelConfiguration : IEntityTypeConfiguration<Model>
    {
        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.ToTable("Model").HasKey(ad => ad.ClientId);

            builder.Property(ad => ad.ClientId).HasColumnName("ClientId").IsRequired();
            builder.Property(ad => ad.Username).HasColumnName("Username");
            builder.Property(ad => ad.SystemId).HasColumnName("SystemId");
        }
    }
}
