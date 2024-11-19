using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretSanta_Core.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_346 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "tblLetterSanta");

            migrationBuilder.CreateTable(
                name: "tblFamilyMember_archive",
                columns: table => new
                {
                    FamilyMemberId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    WarmClothingSize = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    ShoeSize = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: false),
                    Likes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtherRequests = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LetterId = table.Column<int>(type: "int", nullable: false),
                    WarmClothingType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    ShoeSizeType = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFamilyMember_archive", x => x.FamilyMemberId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblFamilyMember_archive");

            migrationBuilder.AddColumn<string>(
                name: "Language",
                table: "tblLetterSanta",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
