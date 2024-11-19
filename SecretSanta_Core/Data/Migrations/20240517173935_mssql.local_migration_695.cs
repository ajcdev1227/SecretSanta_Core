using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretSanta_Core.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_695 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumFriends",
                table: "tblLetterSanta");

            migrationBuilder.CreateTable(
                name: "tblDonations_archive",
                columns: table => new
                {
                    DonNumber = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    DonRecipientNum = table.Column<int>(type: "int", nullable: false),
                    DonAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblHeart_archive",
                columns: table => new
                {
                    RecipientNum = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    AgeType = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Months = table.Column<int>(type: "int", nullable: false),
                    GiftType = table.Column<int>(type: "int", nullable: false),
                    AltGiftType = table.Column<int>(type: "int", nullable: false),
                    AgencyID = table.Column<int>(type: "int", nullable: false),
                    HeartIsActive = table.Column<bool>(type: "bit", nullable: false),
                    DonorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ThankYouRecieved = table.Column<bool>(type: "bit", nullable: false),
                    LastSponsorID = table.Column<int>(type: "int", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    GiftCountInOut = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    FloorGiftCount = table.Column<int>(type: "int", nullable: false),
                    GiftCardCount = table.Column<int>(type: "int", nullable: false),
                    BikeCount = table.Column<int>(type: "int", nullable: false),
                    TotalGiftCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblLetterSanta_archive",
                columns: table => new
                {
                    LetterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Agency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SingleMom = table.Column<bool>(type: "bit", nullable: false),
                    SingleDad = table.Column<bool>(type: "bit", nullable: false),
                    Over65 = table.Column<bool>(type: "bit", nullable: false),
                    Under19 = table.Column<bool>(type: "bit", nullable: false),
                    Disabled = table.Column<bool>(type: "bit", nullable: false),
                    Veteran = table.Column<bool>(type: "bit", nullable: false),
                    Parent = table.Column<bool>(type: "bit", nullable: false),
                    Grandparent = table.Column<bool>(type: "bit", nullable: false),
                    FullTime = table.Column<bool>(type: "bit", nullable: false),
                    PartTime = table.Column<bool>(type: "bit", nullable: false),
                    Calworks = table.Column<bool>(type: "bit", nullable: false),
                    CalFresh = table.Column<bool>(type: "bit", nullable: false),
                    Disability = table.Column<bool>(type: "bit", nullable: false),
                    SocialSecurity = table.Column<bool>(type: "bit", nullable: false),
                    Unemployment = table.Column<bool>(type: "bit", nullable: false),
                    OtherEmployment = table.Column<bool>(type: "bit", nullable: false),
                    KZST = table.Column<bool>(type: "bit", nullable: false),
                    VolunteerCenter = table.Column<bool>(type: "bit", nullable: false),
                    twooneone = table.Column<bool>(type: "bit", nullable: false),
                    NonProfit = table.Column<bool>(type: "bit", nullable: false),
                    FriendFamily = table.Column<bool>(type: "bit", nullable: false),
                    Other = table.Column<bool>(type: "bit", nullable: false),
                    OtherHear = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumChildrenUnder19 = table.Column<int>(type: "int", nullable: true),
                    NumChildrenOver19 = table.Column<int>(type: "int", nullable: true),
                    NumParents = table.Column<int>(type: "int", nullable: true),
                    NumGrandparents = table.Column<int>(type: "int", nullable: true),
                    NumOtherFamily = table.Column<int>(type: "int", nullable: true),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstNeedTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SecondNeedTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ThirdNeedTypeId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DifferentLetter = table.Column<bool>(type: "bit", nullable: false),
                    HouseholdLetter = table.Column<bool>(type: "bit", nullable: false),
                    OutsideHouseholdLetter = table.Column<bool>(type: "bit", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    WriterName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumFriends = table.Column<int>(type: "int", nullable: false),
                    ForWho = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    FosterYouth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false, defaultValueSql: "('')"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    AdoptedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdoptedByPhone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AdoptedByEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LetterSummary = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SantaLetterCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Year = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    FamilyCode = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    AdminGeneralNotes = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    AdminHistoryNotes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AdminGeneralNotes1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LetterReadyDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Santa_archive", x => x.LetterId);
                });

            migrationBuilder.CreateTable(
                name: "tblLetterStatus_archive",
                columns: table => new
                {
                    LetterStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LetterID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    StatusNote = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DateEdited = table.Column<DateTime>(type: "datetime", nullable: false),
                    EditedByUser = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblLetterStatus_archive", x => x.LetterStatusID);
                    table.ForeignKey(
                        name: "FK_tblLetterStatus_tblLetterSanta_archive",
                        column: x => x.LetterID,
                        principalTable: "tblLetterSanta_archive",
                        principalColumn: "LetterId");
                    table.ForeignKey(
                        name: "FK_tblLetterStatus_tblStatusTypes_archive",
                        column: x => x.StatusID,
                        principalTable: "tblStatusTypes",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblLetterStatus_archive_StatusID",
                table: "tblLetterStatus_archive",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblLetterStatus_431DE389AD2A0EADED389F4E4CDD4CE3",
                table: "tblLetterStatus_archive",
                column: "LetterID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDonations_archive");

            migrationBuilder.DropTable(
                name: "tblHeart_archive");

            migrationBuilder.DropTable(
                name: "tblLetterStatus_archive");

            migrationBuilder.DropTable(
                name: "tblLetterSanta_archive");

            migrationBuilder.AddColumn<int>(
                name: "NumFriends",
                table: "tblLetterSanta",
                type: "int",
                nullable: true);
        }
    }
}
