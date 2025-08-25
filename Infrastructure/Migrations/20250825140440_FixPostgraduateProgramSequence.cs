using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixPostgraduateProgramSequence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                SELECT setval('public.""PostgraduatePrograms_Id_seq""', 
                    COALESCE((SELECT MAX(""Id"") FROM public.""PostgraduatePrograms""), 0) + 1, 
                    false);
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
