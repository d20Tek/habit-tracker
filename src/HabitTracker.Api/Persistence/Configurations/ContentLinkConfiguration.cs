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

        AddSeedData(builder);
    }

    private static void AddSeedData(EntityTypeBuilder<ContentLink> builder) =>
        builder.HasData(
            ContentLink.Create(1, "Atomic Habit book", "No matter your goals, Atomic Habits offers a proven framework for improving--every day.", "https://amzn.to/3QXiHV5", 50, "home-sidebar"),
            ContentLink.Create(2, "3-2-1 Newsletter", "How to help someone, the value of bad luck, and rewarding competence", "https://jamesclear.com/3-2-1/march-20-2025", 40, "home-sidebar"),
            ContentLink.Create(5, "History of Habits", "A habit is a routine of behavior that is repeated regularly and tends to occur subconsciously.", "https://en.wikipedia.org/wiki/Habit", 1, "home-sidebar"),
            ContentLink.Create(6, "17 Tips to Build Good Habits", "Habits both good and bad—are closely related to our goals...", "https://www.psychologytoday.com/us/blog/click-here-for-happiness/202106/17-tips-to-build-good-habits", 10, "home-sidebar"),
            ContentLink.Create(7, "3-2-1 Newsletter", "On the surprising path to success...", "https://jamesclear.com/3-2-1/march-27-2025", 5, "home-sidebar"),
            ContentLink.Create(10, "The Power of Habit", "This book explores the science behind habit formation...", "https://amzn.to/3FSTlp6", 60, "home-sidebar"),
            ContentLink.Create(12, "Tiny Habits", "Written by a behavioral scientist from Stanford...", "https://amzn.to/426VXZf", 70, "home-sidebar"),
            ContentLink.Create(14, "7 Habits of Highly Effective People", "The original book on building good habits...", "https://amzn.to/4i2dM02", 80, "home-sidebar"),
            ContentLink.Create(15, "The Neuroscience of Habit Formation", "Delving into the brain science behind habits...", "https://www.joincarbon.com/blog/the-neuroscience-of-habit-formation", 30, "home-sidebar"),
            ContentLink.Create(17, "What Does It Really Take to Build a New Habit?", "This article discusses the distinction between habits and routines...", "https://hbr.org/2021/02/what-does-it-really-take-to-build-a-new-habit", 20, "home-sidebar"),
            ContentLink.Create(18, "3-2-1 Newsletter", "On the joy of losing, how to set expectations with others, and notes to myself", "https://jamesclear.com/3-2-1/april-3-2025", 3, "home-sidebar")
        );
}
