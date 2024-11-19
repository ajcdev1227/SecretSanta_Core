using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecretSanta_Core.Migrations
{
    /// <inheritdoc />
    public partial class mssqllocal_migration_419 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgencyContactModel",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AltPhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstimateWishes = table.Column<int>(type: "int", nullable: true),
                    Roles = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyContactModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationSettings",
                columns: table => new
                {
                    SettingsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SettingsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SettingsValue = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationSettings", x => x.SettingsID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    AgencyId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GiftDetail",
                columns: table => new
                {
                    GiftDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GiftIdeaDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    lblGiftDetail1 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    lblGiftDetail2 = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    GiftDetailOrder = table.Column<int>(type: "int", nullable: false),
                    GiftDetailText = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GiftDetail", x => x.GiftDetailId);
                });

            migrationBuilder.CreateTable(
                name: "SecretSantaLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExceptionType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    ExceptionMessage = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    ExceptionSource = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    StackTrace = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Table_1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblAdminEmails",
                columns: table => new
                {
                    EmailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    EmailContent = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretSantaLetters", x => x.EmailId);
                });

            migrationBuilder.CreateTable(
                name: "tblAgencyCheckInLog",
                columns: table => new
                {
                    CheckInID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CheckInAgencyContactID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CheckInTixNum = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCheckInLog", x => x.CheckInID);
                });

            migrationBuilder.CreateTable(
                name: "tblAgencyContacts",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    Phone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: false),
                    AltPhone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    Fax = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    EstimateWishes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAgencyContacts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblApps",
                columns: table => new
                {
                    AppID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    SetStatusTo = table.Column<int>(type: "int", nullable: true),
                    LaunchForm = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IncludeInLogin = table.Column<bool>(type: "bit", nullable: true),
                    ChangeInfo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblApps", x => x.AppID);
                });

            migrationBuilder.CreateTable(
                name: "tblAppUsers",
                columns: table => new
                {
                    AppUserAU = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AUUserID = table.Column<int>(type: "int", nullable: false),
                    AUAppID = table.Column<int>(type: "int", nullable: false),
                    AUPermission = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false, defaultValueSql: "('User')"),
                    AUStatus = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: false, defaultValueSql: "('Not Allowed')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppUsers", x => x.AppUserAU);
                });

            migrationBuilder.CreateTable(
                name: "tblCategoryDescriptions",
                columns: table => new
                {
                    CategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(9)", unicode: false, maxLength: 9, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblCategoryDescriptions", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "tblDonor",
                columns: table => new
                {
                    DonorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    DonorEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDonor", x => x.DonorId);
                });

            migrationBuilder.CreateTable(
                name: "tblElves",
                columns: table => new
                {
                    ElfID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Elf_FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Elf_LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Elf_Email = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    Elf_Phone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    Elf_AltPhone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TrainingDates = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblElves", x => x.ElfID);
                });

            migrationBuilder.CreateTable(
                name: "tblErrorLog",
                columns: table => new
                {
                    ErrorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AppID = table.Column<int>(type: "int", nullable: true),
                    AppUserID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    ErrorDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Form = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Field = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Event = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LabelID = table.Column<int>(type: "int", nullable: true),
                    MiscVariable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Machine = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErrorLog", x => x.ErrorID);
                });

            migrationBuilder.CreateTable(
                name: "tblErrorLog_archive",
                columns: table => new
                {
                    ErrorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ErrorDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    AppID = table.Column<int>(type: "int", nullable: true),
                    AppUserID = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: true),
                    ErrorDesc = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Form = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Field = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Event = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LabelID = table.Column<int>(type: "int", nullable: true),
                    MiscVariable = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Code = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Machine = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Year = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblErrorLog_archive", x => x.ErrorID);
                });

            migrationBuilder.CreateTable(
                name: "tblLetterSanta",
                columns: table => new
                {
                    LetterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Language = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Zip = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Agency = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    NumChildrenUnder19 = table.Column<int>(type: "int", nullable: true),
                    NumChildrenOver19 = table.Column<int>(type: "int", nullable: true),
                    NumParents = table.Column<int>(type: "int", nullable: true),
                    NumGrandparents = table.Column<int>(type: "int", nullable: true),
                    NumOtherFamily = table.Column<int>(type: "int", nullable: true),
                    Letter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    WriterName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NumFriends = table.Column<int>(type: "int", nullable: true),
                    Phone = table.Column<string>(type: "varchar(12)", unicode: false, maxLength: 12, nullable: false, defaultValueSql: "('')"),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    FamilyCode = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    AdminGeneralNotes = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: true),
                    AdoptedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdoptedByPhone = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AdoptedByEmail = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AdminHistoryNotes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LetterSummary = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    AdminGeneralNotes1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    LetterReadyDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PickupDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_Santa", x => x.LetterId);
                });

            migrationBuilder.CreateTable(
                name: "tblMessage",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageTitle = table.Column<string>(type: "varchar(250)", unicode: false, maxLength: 250, nullable: false),
                    MessageContent = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.MessageId);
                });

            migrationBuilder.CreateTable(
                name: "tblNeedTypes",
                columns: table => new
                {
                    NeedTypeId = table.Column<int>(type: "int", nullable: false),
                    NeedTypeString = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tblNeedT__18F2CFA5DC7621F9", x => x.NeedTypeId);
                });

            migrationBuilder.CreateTable(
                name: "tblRegions",
                columns: table => new
                {
                    RegionShort = table.Column<string>(type: "nchar(4)", fixedLength: true, maxLength: 4, nullable: false),
                    RegionLong = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRegions", x => x.RegionShort);
                });

            migrationBuilder.CreateTable(
                name: "tblRoles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRoles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tblStatusTypes",
                columns: table => new
                {
                    StatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    StatusDescription = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    StatusSortNumber = table.Column<int>(type: "int", nullable: true),
                    StatusObsolete = table.Column<bool>(type: "bit", nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    RecipientChild = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    Letters = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    ChartGroup1 = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    ChartGroup2 = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true),
                    WebGroup = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RecipientWebGroup = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStatusTypes", x => x.StatusID);
                });

            migrationBuilder.CreateTable(
                name: "tblStory",
                columns: table => new
                {
                    StoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StoryContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoryIUd", x => x.StoryId);
                });

            migrationBuilder.CreateTable(
                name: "tblUsers",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserFirstName = table.Column<string>(type: "nchar(30)", fixedLength: true, maxLength: 30, nullable: false),
                    UserLastName = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: false),
                    UserName = table.Column<string>(type: "nchar(100)", fixedLength: true, maxLength: 100, nullable: false),
                    UserPassword = table.Column<string>(type: "nchar(25)", fixedLength: true, maxLength: 25, nullable: false),
                    UserDateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UserAdmin = table.Column<bool>(type: "bit", nullable: false),
                    UserIsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CellPhone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    UserMessage = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAppUserTest", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "tblVolunteerLog",
                columns: table => new
                {
                    VolLogNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VolID = table.Column<int>(type: "int", nullable: false),
                    FreeField = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TimeOut = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeIn = table.Column<DateTime>(type: "datetime", nullable: true),
                    Company = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Site = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tblVolunteerNames",
                columns: table => new
                {
                    VolID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FreeField = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVolunteerNames", x => x.VolID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tblDonorThankYou",
                columns: table => new
                {
                    DonorThankYouId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThankYouDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    RecipientNum = table.Column<int>(type: "int", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DonorThankYou", x => x.DonorThankYouId);
                    table.ForeignKey(
                        name: "FK_tblDonorThankYou_tblDonor",
                        column: x => x.DonorId,
                        principalTable: "tblDonor",
                        principalColumn: "DonorId");
                });

            migrationBuilder.CreateTable(
                name: "tblFamilyMember",
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
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblFamilyMember", x => x.FamilyMemberId);
                    table.ForeignKey(
                        name: "FK_tblFamilyMember_tblLetterSanta",
                        column: x => x.LetterId,
                        principalTable: "tblLetterSanta",
                        principalColumn: "LetterId");
                });

            migrationBuilder.CreateTable(
                name: "tblMessageContact",
                columns: table => new
                {
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    ContactId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageContact", x => new { x.MessageId, x.ContactId });
                    table.ForeignKey(
                        name: "FK_tblMessageContact_tblAgencyContacts",
                        column: x => x.ContactId,
                        principalTable: "tblAgencyContacts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblMessageContact_tblMessage",
                        column: x => x.MessageId,
                        principalTable: "tblMessage",
                        principalColumn: "MessageId");
                });

            migrationBuilder.CreateTable(
                name: "tblAgencies",
                columns: table => new
                {
                    AgencyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyName = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    AgencyCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    AgencyStreet = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyCity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    AgencyState = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    AgencyZip = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Type = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    EstimateWishes = table.Column<int>(type: "int", nullable: true),
                    Payment = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: true),
                    Overflow = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Website = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    OverflowNotes = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    NotificationLevel = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((10))"),
                    Region = table.Column<string>(type: "nchar(4)", fixedLength: true, maxLength: 4, nullable: false, defaultValueSql: "(N'SR')"),
                    AgencyWebRank = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAgencies", x => x.AgencyID);
                    table.ForeignKey(
                        name: "FK_tblAgencies_tblRegions",
                        column: x => x.Region,
                        principalTable: "tblRegions",
                        principalColumn: "RegionShort");
                });

            migrationBuilder.CreateTable(
                name: "tblSponsors",
                columns: table => new
                {
                    SponsorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SponsorName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SponsorStreet = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SponsorCity = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    SponsorState = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    SponsorZip = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    SS_FirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SS_LastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    SS_Phone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    SS_AltPhone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    PrimaryFirstName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PrimaryLastName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    PrimaryPhone = table.Column<string>(type: "varchar(14)", unicode: false, maxLength: 14, nullable: true),
                    PrimaryEmail = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ElfID = table.Column<int>(type: "int", nullable: true),
                    SS_Email = table.Column<string>(type: "varchar(75)", unicode: false, maxLength: 75, nullable: true),
                    DisplayType = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    DisplaySignage = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    DeliveryDate = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    SongRequest = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    CommunitySponsor = table.Column<bool>(type: "bit", nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    AdoptAHeartDisplay = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    LYGiftAssigned = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    LYGiftOut = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Region = table.Column<string>(type: "nchar(4)", fixedLength: true, maxLength: 4, nullable: false, defaultValueSql: "(N'SR')"),
                    OpenToPublic = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblSponsors", x => x.SponsorID);
                    table.ForeignKey(
                        name: "FK_tblSponsors_tblElves",
                        column: x => x.ElfID,
                        principalTable: "tblElves",
                        principalColumn: "ElfID");
                    table.ForeignKey(
                        name: "FK_tblSponsors_tblRegions",
                        column: x => x.Region,
                        principalTable: "tblRegions",
                        principalColumn: "RegionShort");
                });

            migrationBuilder.CreateTable(
                name: "tblLetterStatus",
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
                    table.PrimaryKey("PK_tblLetterStatus", x => x.LetterStatusID);
                    table.ForeignKey(
                        name: "FK_tblLetterStatus_tblLetterSanta",
                        column: x => x.LetterID,
                        principalTable: "tblLetterSanta",
                        principalColumn: "LetterId");
                    table.ForeignKey(
                        name: "FK_tblLetterStatus_tblStatusTypes",
                        column: x => x.StatusID,
                        principalTable: "tblStatusTypes",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "tblViewHeaders",
                columns: table => new
                {
                    ViewID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewDescription = table.Column<string>(type: "nchar(75)", fixedLength: true, maxLength: 75, nullable: false),
                    ViewCreater_UserID = table.Column<int>(type: "int", nullable: false),
                    AndOrMode = table.Column<string>(type: "nchar(3)", fixedLength: true, maxLength: 3, nullable: false),
                    Gender_Search = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    GiftType_Search = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    GiftDescription1_Search = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    GiftDescription2_Search = table.Column<string>(type: "nchar(20)", fixedLength: true, maxLength: 20, nullable: true),
                    AgeFrom_Search = table.Column<int>(type: "int", nullable: true),
                    AgeTo_Search = table.Column<int>(type: "int", nullable: true),
                    Bike_Search = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    GiftCard_Search = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true),
                    BigItem_Search = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblViewHeaders", x => x.ViewID);
                    table.ForeignKey(
                        name: "FK_tblViewHeaders_tblUsers",
                        column: x => x.ViewCreater_UserID,
                        principalTable: "tblUsers",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "tblAgencyLocations",
                columns: table => new
                {
                    tblAgencyLocationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    LocDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAgencyLocations", x => x.tblAgencyLocationID);
                    table.ForeignKey(
                        name: "FK_tblAgencyLocations_tblAgencies",
                        column: x => x.AgencyID,
                        principalTable: "tblAgencies",
                        principalColumn: "AgencyID");
                });

            migrationBuilder.CreateTable(
                name: "tblAgencyPickUp",
                columns: table => new
                {
                    AgencyPickUpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblAgencyPickUp", x => x.AgencyPickUpId);
                    table.ForeignKey(
                        name: "FK_tblAgencyPickUp_tblAgencies",
                        column: x => x.AgencyId,
                        principalTable: "tblAgencies",
                        principalColumn: "AgencyID");
                });

            migrationBuilder.CreateTable(
                name: "tblRecipientParent",
                columns: table => new
                {
                    RecipientNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    AgeType = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true, defaultValueSql: "('years')"),
                    Gender = table.Column<string>(type: "varchar(2)", unicode: false, maxLength: 2, nullable: true),
                    RecipientInfo = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    GiftWish = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    GiftType = table.Column<int>(type: "int", nullable: true),
                    GiftDetail1 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    GiftDetail2 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AltGiftWish = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    AltGiftType = table.Column<int>(type: "int", nullable: true),
                    AltGiftDetail1 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AltGiftDetail2 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AgencyID = table.Column<int>(type: "int", nullable: true),
                    Resubmit = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    EditNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DonorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DonorRegisterDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEntered = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    WebRank = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    FloorGiftCount = table.Column<int>(type: "int", nullable: true),
                    GiftCardCount = table.Column<int>(type: "int", nullable: true),
                    BikeCount = table.Column<int>(type: "int", nullable: true),
                    TotalGiftCount = table.Column<int>(type: "int", nullable: true),
                    PrintTracker = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ApproveTracker = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    RecParFreefield = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRecipientParent", x => x.RecipientNum);
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_AltGiftDetail",
                        column: x => x.AltGiftType,
                        principalTable: "GiftDetail",
                        principalColumn: "GiftDetailId");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_GiftDetail",
                        column: x => x.GiftType,
                        principalTable: "GiftDetail",
                        principalColumn: "GiftDetailId");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_tblAgencies",
                        column: x => x.AgencyID,
                        principalTable: "tblAgencies",
                        principalColumn: "AgencyID");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_tblDonor",
                        column: x => x.DonorId,
                        principalTable: "tblDonor",
                        principalColumn: "DonorId");
                });

            migrationBuilder.CreateTable(
                name: "tblRecipientParent_archive",
                columns: table => new
                {
                    RecipientNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    AgeType = table.Column<string>(type: "varchar(6)", unicode: false, maxLength: 6, nullable: true),
                    Gender = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    RecipientInfo = table.Column<string>(type: "varchar(35)", unicode: false, maxLength: 35, nullable: true),
                    GiftWish = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    GiftType = table.Column<int>(type: "int", nullable: true),
                    GiftDetail1 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    GiftDetail2 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AltGiftWish = table.Column<string>(type: "varchar(25)", unicode: false, maxLength: 25, nullable: true),
                    AltGiftType = table.Column<int>(type: "int", nullable: true),
                    AltGiftDetail1 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AltGiftDetail2 = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    AgencyID = table.Column<int>(type: "int", nullable: true),
                    SponsorID = table.Column<int>(type: "int", nullable: true),
                    Resubmit = table.Column<bool>(type: "bit", nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    EditNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: true),
                    Year = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    DonorId = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    DonorRegisterDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DateEntered = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "('11/1/2019')"),
                    Freefield = table.Column<string>(type: "nchar(50)", fixedLength: true, maxLength: 50, nullable: true),
                    RecGiftCountGiftInOut = table.Column<int>(type: "int", nullable: true),
                    ThankYouRecieved = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    WebRank = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRecipientParent_archive", x => x.RecipientNum);
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_AltGiftDetail_archive",
                        column: x => x.AltGiftType,
                        principalTable: "GiftDetail",
                        principalColumn: "GiftDetailId");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_GiftDetail_archive",
                        column: x => x.GiftType,
                        principalTable: "GiftDetail",
                        principalColumn: "GiftDetailId");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_tblAgencies_archive",
                        column: x => x.AgencyID,
                        principalTable: "tblAgencies",
                        principalColumn: "AgencyID");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_tblDonor_archive",
                        column: x => x.DonorId,
                        principalTable: "tblDonor",
                        principalColumn: "DonorId");
                    table.ForeignKey(
                        name: "FK_tblRecipientParent_tblSponsors_archive",
                        column: x => x.SponsorID,
                        principalTable: "tblSponsors",
                        principalColumn: "SponsorID");
                });

            migrationBuilder.CreateTable(
                name: "tblViewItems_Agency",
                columns: table => new
                {
                    ViewAgencyKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewID = table.Column<int>(type: "int", nullable: false),
                    AgencyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblViewItemsAgency", x => x.ViewAgencyKey);
                    table.ForeignKey(
                        name: "FK_tblViewItems_Agency_tblAgencies",
                        column: x => x.AgencyID,
                        principalTable: "tblAgencies",
                        principalColumn: "AgencyID");
                    table.ForeignKey(
                        name: "FK_tblViewItems_Agency_tblViewHeaders",
                        column: x => x.ViewID,
                        principalTable: "tblViewHeaders",
                        principalColumn: "ViewID");
                });

            migrationBuilder.CreateTable(
                name: "tblViewItems_Sponsor",
                columns: table => new
                {
                    ViewSponsorKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewID = table.Column<int>(type: "int", nullable: false),
                    SponsorID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblViewItems_Sponsor", x => x.ViewSponsorKey);
                    table.ForeignKey(
                        name: "FK_tblViewItems_Sponsor_tblSponsors",
                        column: x => x.SponsorID,
                        principalTable: "tblSponsors",
                        principalColumn: "SponsorID");
                    table.ForeignKey(
                        name: "FK_tblViewItems_Sponsor_tblViewHeaders",
                        column: x => x.ViewID,
                        principalTable: "tblViewHeaders",
                        principalColumn: "ViewID");
                });

            migrationBuilder.CreateTable(
                name: "tblViewItems_Status",
                columns: table => new
                {
                    ViewStatusKey = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViewID = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblViewItems_Status", x => x.ViewStatusKey);
                    table.ForeignKey(
                        name: "FK_tblViewItems_Status_tblStatusTypes",
                        column: x => x.StatusID,
                        principalTable: "tblStatusTypes",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_tblViewItems_Status_tblViewHeaders",
                        column: x => x.ViewID,
                        principalTable: "tblViewHeaders",
                        principalColumn: "ViewID");
                });

            migrationBuilder.CreateTable(
                name: "tblDonations",
                columns: table => new
                {
                    DonId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientNum = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonDateTime = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDonations", x => x.DonId);
                    table.ForeignKey(
                        name: "FK_tblDonations_tblRecipientParent",
                        column: x => x.RecipientNum,
                        principalTable: "tblRecipientParent",
                        principalColumn: "RecipientNum");
                });

            migrationBuilder.CreateTable(
                name: "tblRecipientChild",
                columns: table => new
                {
                    LabelNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientNum = table.Column<int>(type: "int", nullable: false),
                    Primary = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    HistoryNotes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BulkEditTracker = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    BulkEditTracker2 = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    RecChildFreefield = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRecipientChild", x => x.LabelNum);
                    table.ForeignKey(
                        name: "FK_tblRecipientChild_tblRecipientParent",
                        column: x => x.RecipientNum,
                        principalTable: "tblRecipientParent",
                        principalColumn: "RecipientNum");
                });

            migrationBuilder.CreateTable(
                name: "tblRecipientChild_archive",
                columns: table => new
                {
                    LabelNum = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipientNum = table.Column<int>(type: "int", nullable: false),
                    BigItem = table.Column<bool>(type: "bit", nullable: false),
                    BikeReceived = table.Column<bool>(type: "bit", nullable: false),
                    GiftCard = table.Column<bool>(type: "bit", nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    Primary = table.Column<bool>(type: "bit", nullable: false),
                    Year = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    GiftCount = table.Column<int>(type: "int", nullable: true),
                    LastStatus = table.Column<int>(type: "int", nullable: true),
                    FreeField = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: true),
                    BulkStatusChange = table.Column<string>(type: "nchar(5)", fixedLength: true, maxLength: 5, nullable: true),
                    HistoryNotes = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    EditNewRecipient = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    PrintHeart = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    PrintTag = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    BulkEdit = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    ConfirmHeartPrint = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    ConfirmTagPrint = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    HeartCentralGiftCode = table.Column<string>(type: "nchar(1)", fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblRecipientChild_archive", x => x.LabelNum);
                    table.ForeignKey(
                        name: "FK_tblRecipientChild_tblRecipientParent_archive",
                        column: x => x.RecipientNum,
                        principalTable: "tblRecipientParent_archive",
                        principalColumn: "RecipientNum");
                });

            migrationBuilder.CreateTable(
                name: "tblStatusLog",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelNum = table.Column<int>(type: "int", nullable: false),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AppID = table.Column<int>(type: "int", nullable: false),
                    ChangeInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastSponsorID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStatusLog", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblApps",
                        column: x => x.AppID,
                        principalTable: "tblApps",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblRecipientChild",
                        column: x => x.LabelNum,
                        principalTable: "tblRecipientChild",
                        principalColumn: "LabelNum",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblStatusTypes",
                        column: x => x.StatusID,
                        principalTable: "tblStatusTypes",
                        principalColumn: "StatusID");
                });

            migrationBuilder.CreateTable(
                name: "tblStatusLog_archive",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LabelNum = table.Column<int>(type: "int", nullable: true),
                    StatusID = table.Column<int>(type: "int", nullable: false),
                    DateEdited = table.Column<DateTime>(type: "datetime", nullable: false),
                    EditedByUser = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    AppID = table.Column<int>(type: "int", nullable: false),
                    Row_Version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    ChangeInfo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    EditedByAppUser = table.Column<int>(type: "int", nullable: true),
                    EditedByAppDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    FreeField = table.Column<int>(type: "int", nullable: true),
                    Machine = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Year = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblStatusLog_archive", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblApps_archive",
                        column: x => x.AppID,
                        principalTable: "tblApps",
                        principalColumn: "AppID");
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblRecipientChild_archive",
                        column: x => x.LabelNum,
                        principalTable: "tblRecipientChild_archive",
                        principalColumn: "LabelNum");
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblStatusTypes_archive",
                        column: x => x.StatusID,
                        principalTable: "tblStatusTypes",
                        principalColumn: "StatusID");
                    table.ForeignKey(
                        name: "FK_tblStatusLog_tblUsers_archive",
                        column: x => x.EditedByAppUser,
                        principalTable: "tblUsers",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tblAgencies_Region",
                table: "tblAgencies",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "UC_tblAgencies_AgencyCode",
                table: "tblAgencies",
                column: "AgencyCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UC_tblAgencies_AgencyName",
                table: "tblAgencies",
                column: "AgencyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblAgencyLocations_AgencyID",
                table: "tblAgencyLocations",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_tblAgencyPickUp_AgencyId",
                table: "tblAgencyPickUp",
                column: "AgencyId");

            migrationBuilder.CreateIndex(
                name: "IX_tblDonations_RecipientNum",
                table: "tblDonations",
                column: "RecipientNum");

            migrationBuilder.CreateIndex(
                name: "IX_tblDonorThankYou_DonorId",
                table: "tblDonorThankYou",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblFamilyMember_LetterId",
                table: "tblFamilyMember",
                column: "LetterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLetterStatus_StatusID",
                table: "tblLetterStatus",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblLetterStatus_431DE389AD2A0EADED389F4E4CDD4CE3",
                table: "tblLetterStatus",
                column: "LetterID");

            migrationBuilder.CreateIndex(
                name: "IX_tblMessageContact_ContactId",
                table: "tblMessageContact",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblRecipientChild_F676665CB29E6A64BB3F461258164B4E",
                table: "tblRecipientChild",
                columns: new[] { "RecipientNum", "Primary" });

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientChild_archive_RecipientNum",
                table: "tblRecipientChild_archive",
                column: "RecipientNum");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_AltGiftType",
                table: "tblRecipientParent",
                column: "AltGiftType");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_GiftType",
                table: "tblRecipientParent",
                column: "GiftType");

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblRecipientParent_44C63202CD904BBFF53DE74223D6A6B7",
                table: "tblRecipientParent",
                columns: new[] { "DonorId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblRecipientParent_8DB3CC20934C2198DA4B4BF14E2A8383",
                table: "tblRecipientParent",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_archive_AgencyID",
                table: "tblRecipientParent_archive",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_archive_AltGiftType",
                table: "tblRecipientParent_archive",
                column: "AltGiftType");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_archive_DonorId",
                table: "tblRecipientParent_archive",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_archive_GiftType",
                table: "tblRecipientParent_archive",
                column: "GiftType");

            migrationBuilder.CreateIndex(
                name: "IX_tblRecipientParent_archive_SponsorID",
                table: "tblRecipientParent_archive",
                column: "SponsorID");

            migrationBuilder.CreateIndex(
                name: "IX_tblRegions_1",
                table: "tblRegions",
                column: "RegionShort",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblSponsors_Region",
                table: "tblSponsors",
                column: "Region");

            migrationBuilder.CreateIndex(
                name: "NonClusteredIndex-tblSponsors-ElfID",
                table: "tblSponsors",
                column: "ElfID");

            migrationBuilder.CreateIndex(
                name: "IX_tblStatusLog_AppID",
                table: "tblStatusLog",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblStatusLog_CACBFEF9C020D3B63A46A19646FD8908",
                table: "tblStatusLog",
                column: "LabelNum");

            migrationBuilder.CreateIndex(
                name: "nci_wi_tblStatusLog_F8759D1A6B87A674B38E3C8B5FA96258",
                table: "tblStatusLog",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tblStatusLog_archive_AppID",
                table: "tblStatusLog_archive",
                column: "AppID");

            migrationBuilder.CreateIndex(
                name: "IX_tblStatusLog_archive_EditedByAppUser",
                table: "tblStatusLog_archive",
                column: "EditedByAppUser");

            migrationBuilder.CreateIndex(
                name: "IX_tblStatusLog_archive_LabelNum",
                table: "tblStatusLog_archive",
                column: "LabelNum");

            migrationBuilder.CreateIndex(
                name: "IX_tblStatusLog_archive_StatusID",
                table: "tblStatusLog_archive",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewHeaders",
                table: "tblViewHeaders",
                column: "ViewDescription",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblViewHeaders_ViewCreater_UserID",
                table: "tblViewHeaders",
                column: "ViewCreater_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewItems_Agency_AgencyID",
                table: "tblViewItems_Agency",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewItems_Agency_ViewID",
                table: "tblViewItems_Agency",
                column: "ViewID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewItems_Sponsor_SponsorID",
                table: "tblViewItems_Sponsor",
                column: "SponsorID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewItems_Sponsor_ViewID",
                table: "tblViewItems_Sponsor",
                column: "ViewID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewItems_Status_StatusID",
                table: "tblViewItems_Status",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_tblViewItems_Status_ViewID",
                table: "tblViewItems_Status",
                column: "ViewID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyContactModel");

            migrationBuilder.DropTable(
                name: "ApplicationSettings");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "SecretSantaLog");

            migrationBuilder.DropTable(
                name: "tblAdminEmails");

            migrationBuilder.DropTable(
                name: "tblAgencyCheckInLog");

            migrationBuilder.DropTable(
                name: "tblAgencyLocations");

            migrationBuilder.DropTable(
                name: "tblAgencyPickUp");

            migrationBuilder.DropTable(
                name: "tblAppUsers");

            migrationBuilder.DropTable(
                name: "tblCategoryDescriptions");

            migrationBuilder.DropTable(
                name: "tblDonations");

            migrationBuilder.DropTable(
                name: "tblDonorThankYou");

            migrationBuilder.DropTable(
                name: "tblErrorLog");

            migrationBuilder.DropTable(
                name: "tblErrorLog_archive");

            migrationBuilder.DropTable(
                name: "tblFamilyMember");

            migrationBuilder.DropTable(
                name: "tblLetterStatus");

            migrationBuilder.DropTable(
                name: "tblMessageContact");

            migrationBuilder.DropTable(
                name: "tblNeedTypes");

            migrationBuilder.DropTable(
                name: "tblRoles");

            migrationBuilder.DropTable(
                name: "tblStatusLog");

            migrationBuilder.DropTable(
                name: "tblStatusLog_archive");

            migrationBuilder.DropTable(
                name: "tblStory");

            migrationBuilder.DropTable(
                name: "tblViewItems_Agency");

            migrationBuilder.DropTable(
                name: "tblViewItems_Sponsor");

            migrationBuilder.DropTable(
                name: "tblViewItems_Status");

            migrationBuilder.DropTable(
                name: "tblVolunteerLog");

            migrationBuilder.DropTable(
                name: "tblVolunteerNames");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tblLetterSanta");

            migrationBuilder.DropTable(
                name: "tblAgencyContacts");

            migrationBuilder.DropTable(
                name: "tblMessage");

            migrationBuilder.DropTable(
                name: "tblRecipientChild");

            migrationBuilder.DropTable(
                name: "tblApps");

            migrationBuilder.DropTable(
                name: "tblRecipientChild_archive");

            migrationBuilder.DropTable(
                name: "tblStatusTypes");

            migrationBuilder.DropTable(
                name: "tblViewHeaders");

            migrationBuilder.DropTable(
                name: "tblRecipientParent");

            migrationBuilder.DropTable(
                name: "tblRecipientParent_archive");

            migrationBuilder.DropTable(
                name: "tblUsers");

            migrationBuilder.DropTable(
                name: "GiftDetail");

            migrationBuilder.DropTable(
                name: "tblAgencies");

            migrationBuilder.DropTable(
                name: "tblDonor");

            migrationBuilder.DropTable(
                name: "tblSponsors");

            migrationBuilder.DropTable(
                name: "tblElves");

            migrationBuilder.DropTable(
                name: "tblRegions");
        }
    }
}
