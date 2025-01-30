using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MsRulesEngine.DBConfigurations;

public class ArcadeDbConfiguration : IEntityTypeConfiguration<Arcade>
{
    public void Configure(EntityTypeBuilder<Arcade> builder)
    {
        builder
            .HasKey(a => a.Id);
        builder.Property(a => a.Workflows)
            .HasMaxLength(4000);
    }
}