using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HabitTracker.Api.Persistence.Configurations;

internal class ContentLinkConfiguration : IEntityTypeConfiguration<ContentLink>
{
    public void Configure(EntityTypeBuilder<ContentLink> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(c => c.Id)
              .ValueGeneratedOnAdd();

        builder.Property(l => l.Title)
            .IsRequired()
            .HasMaxLength(Constants.ContentLinks.TitleLength);

        builder.Property(l => l.Description)
            .HasMaxLength(Constants.ContentLinks.DescLength);

        builder.Property(l => l.Url)
            .IsRequired()
            .HasMaxLength(Constants.ContentLinks.UrlLength);

        builder.Property(l => l.SortOrder)
            .IsRequired();

        builder.Property(l => l.Group)
            .IsRequired()
            .HasMaxLength(Constants.ContentLinks.GroupLength);
    }
}
