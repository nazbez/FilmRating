using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmRating.Migrations
{
    /// <inheritdoc />
    public partial class IsFavourite_feature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFavourite",
                table: "Rating",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFavourite",
                table: "Rating");
        }
    }
}
