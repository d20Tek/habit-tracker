using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitTracker.Api.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddAnotherContentLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ContentLinks",
                columns: new[] { "Id", "Description", "Group", "SortOrder", "Title", "Url" },
                values: new object[] { 18, "On the joy of losing, how to set expectations with others, and notes to myself", "home-sidebar", 3, "3-2-1 Newsletter", "https://jamesclear.com/3-2-1/april-3-2025" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ContentLinks",
                keyColumn: "Id",
                keyValue: 18);
        }
    }
}
