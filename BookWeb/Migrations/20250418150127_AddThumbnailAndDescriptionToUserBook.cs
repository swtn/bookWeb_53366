using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddThumbnailAndDescriptionToUserBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserBooks",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Rating",
                table: "UserBooks",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Thumbnail",
                table: "UserBooks",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "UserBooks");

            migrationBuilder.DropColumn(
                name: "Thumbnail",
                table: "UserBooks");
        }
    }
}
