using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class newtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProfile",
                table: "StudentProfile");

            migrationBuilder.RenameTable(
                name: "StudentProfile",
                newName: "StudentProfiles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PostgraduatePrograms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProgramName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    DegreeType = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    StudyMode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Field = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostgraduatePrograms", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostgraduatePrograms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StudentProfiles",
                table: "StudentProfiles");

            migrationBuilder.RenameTable(
                name: "StudentProfiles",
                newName: "StudentProfile");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StudentProfile",
                table: "StudentProfile",
                column: "Id");
        }
    }
}
