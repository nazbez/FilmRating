using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmRating.Migrations
{
    /// <inheritdoc />
    public partial class Add_Film : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ShortDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Budget = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    DirectorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Film_Artist_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Film_Genre_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Film_Actor",
                columns: table => new
                {
                    ActorId = table.Column<int>(type: "int", nullable: false),
                    FilmId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film_Actor", x => new { x.ActorId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_Film_Actor_Artist_ActorId",
                        column: x => x.FilmId,
                        principalTable: "Artist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Film_Actor_Film_FilmId",
                        column: x => x.ActorId,
                        principalTable: "Film",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Film_DirectorId",
                table: "Film",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Film_GenreId",
                table: "Film",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Film_Actor_ActorId",
                table: "Film_Actor",
                column: "ActorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Film_Actor");

            migrationBuilder.DropTable(
                name: "Film");
        }
    }
}
