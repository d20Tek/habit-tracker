using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitTracker.Api.Persistence.Configurations;

public class WeighingConfiguration : IEntityTypeConfiguration<Weighing>
{
    public void Configure(EntityTypeBuilder<Weighing> builder)
    {
        builder.HasKey(c => c.WeighingId);

        builder.Property(c => c.WeighingId)
               .ValueGeneratedOnAdd();

        builder.HasIndex(w => new { w.UserId, w.Date })
               .IsUnique();

        builder.Property(c => c.UserId)
              .IsRequired()
              .HasMaxLength(Constants.Weighings.UserIdLength);

        builder.Property(c => c.Weight)
               .HasColumnType("decimal(5,2)")
               .IsRequired();
    }
}
