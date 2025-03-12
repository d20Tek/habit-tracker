using HabitTracker.Api.Common;
using HabitTracker.Api.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitTracker.Api.Persistence.Configurations;

internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);

        builder.Property(c => c.CategoryId)
              .ValueGeneratedOnAdd();

        builder.Property(c => c.UserId)
              .IsRequired()
              .HasMaxLength(Constants.Categories.UserIdLength);

        builder.Property(c => c.Name)
              .IsRequired()
              .HasMaxLength(Constants.Categories.NameLength);
    }
}
