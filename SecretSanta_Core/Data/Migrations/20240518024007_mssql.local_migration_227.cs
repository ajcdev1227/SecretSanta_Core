using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretSanta_Core.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_227 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LastSponsorID",
                table: "tblStatusLog_archive",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastSponsorID",
                table: "tblStatusLog_archive");
        }
    }
}
