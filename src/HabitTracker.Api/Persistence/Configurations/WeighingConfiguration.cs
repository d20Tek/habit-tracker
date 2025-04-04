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

        builder.Property(hc => hc.Date)
               .IsRequired()
               .HasConversion(
                    // convert DateTimeOffset back and forth to string for Sqlite support.
                    v => v.ToString(DbConstants.DateTimeOffsetFormat),
                    v => DateTimeOffset.Parse(v)
               );

        builder.Property(c => c.Weight)
               .HasColumnType(DbConstants.WeightFormat)
               .IsRequired();
    }
}
