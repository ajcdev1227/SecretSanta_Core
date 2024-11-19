using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretSanta_Core.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_874 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "twooneone",
                table: "tblLetterSanta_archive",
                newName: "211");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "211",
                table: "tblLetterSanta_archive",
                newName: "twooneone");
        }
    }
}
