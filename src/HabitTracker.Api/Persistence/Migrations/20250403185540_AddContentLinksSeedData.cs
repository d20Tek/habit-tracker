using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HabitTracker.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddContentLinksSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContentLinks",
                columns: new[] { "Id", "Description", "Group", "SortOrder", "Title", "Url" },
                values: new object[,]
                {
                    { 1, "No matter your goals, Atomic Habits offers a proven framework for improving--every day.", "home-sidebar", 50, "Atomic Habit book", "https://amzn.to/3QXiHV5" },
                    { 2, "How to help someone, the value of bad luck, and rewarding competence", "home-sidebar", 40, "3-2-1 Newsletter", "https://jamesclear.com/3-2-1/march-20-2025" },
                    { 5, "A habit is a routine of behavior that is repeated regularly and tends to occur subconsciously.", "home-sidebar", 1, "History of Habits", "https://en.wikipedia.org/wiki/Habit" },
                    { 6, "Habits both good and bad—are closely related to our goals...", "home-sidebar", 10, "17 Tips to Build Good Habits", "https://www.psychologytoday.com/us/blog/click-here-for-happiness/202106/17-tips-to-build-good-habits" },
                    { 7, "On the surprising path to success...", "home-sidebar", 5, "3-2-1 Newsletter", "https://jamesclear.com/3-2-1/march-27-2025" },
                    { 10, "This book explores the science behind habit formation...", "home-sidebar", 60, "The Power of Habit", "https://amzn.to/3FSTlp6" },
                    { 12, "Written by a behavioral scientist from Stanford...", "home-sidebar", 70, "Tiny Habits", "https://amzn.to/426VXZf" },
                    { 14, "The original book on building good habits...", "home-sidebar", 80, "7 Habits of Highly Effective People", "https://amzn.to/4i2dM02" },
                    { 15, "Delving into the brain science behind habits...", "home-sidebar", 30, "The Neuroscience of Habit Formation", "https://www.joincarbon.com/blog/the-neuroscience-of-habit-formation" },
                    { 17, "This article discusses the distinction between habits and routines...", "home-sidebar", 20, "What Does It Really Take to Build a New Habit?", "https://hbr.org/2021/02/what-does-it-really-take-to-build-a-new-habit" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 17);
        }
    }
}
