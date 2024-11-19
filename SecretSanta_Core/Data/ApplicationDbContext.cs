using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ApplicationSetting> ApplicationSettings { get; set; }

        public virtual DbSet<GiftDetail> GiftDetails { get; set; }

        public virtual DbSet<SecretSantaLog> SecretSantaLogs { get; set; }

        public virtual DbSet<TblAdminEmail> TblAdminEmails { get; set; }

        public virtual DbSet<TblAgency> TblAgencies { get; set; }

        public virtual DbSet<TblAgencyCheckInLog> TblAgencyCheckInLogs { get; set; }

        public virtual DbSet<TblAgencyContact> TblAgencyContacts { get; set; }

        public virtual DbSet<TblAgencyLocation> TblAgencyLocations { get; set; }

        public virtual DbSet<TblApp> TblApps { get; set; }

        public virtual DbSet<TblAppUser> TblAppUsers { get; set; }

        public virtual DbSet<TblCategoryDescription> TblCategoryDescriptions { get; set; }

        public virtual DbSet<TblDonation> TblDonations { get; set; }

        public virtual DbSet<TblDonationArchive> TblDonationArchives { get; set; }

        public virtual DbSet<TblDonor> TblDonors { get; set; }

        public virtual DbSet<TblDonorThankYou> TblDonorThankYous { get; set; }

        public virtual DbSet<TblElf> TblElves { get; set; }

        public virtual DbSet<TblErrorLog> TblErrorLogs { get; set; }

        public virtual DbSet<TblErrorLogArchive> TblErrorLogArchives { get; set; }

        public virtual DbSet<TblFamilyMember> TblFamilyMembers { get; set; }

        public virtual DbSet<TblFamilyMemberArchive> TblFamilyMemberArchives { get; set; }

        public virtual DbSet<TblHeartArchive> TblHeartArchives { get; set; }

        public virtual DbSet<TblLetterSanta> TblLetterSanta { get; set; }

        public virtual DbSet<TblLetterSantaArchive> TblLetterSantaArchives { get; set; }

        public virtual DbSet<TblLetterStatus> TblLetterStatuses { get; set; }

        public virtual DbSet<TblLetterStatusArchive> TblLetterStatusArchives { get; set; }

        public virtual DbSet<TblMessage> TblMessages { get; set; }

        public virtual DbSet<TblMessageContact> TblMessageContacts { get; set; }

        public virtual DbSet<TblNeedType> TblNeedTypes { get; set; }

        public virtual DbSet<TblRecipientChild> TblRecipientChildren { get; set; }

        public virtual DbSet<TblRecipientChildArchive> TblRecipientChildArchives { get; set; }

        public virtual DbSet<TblRecipientParent> TblRecipientParents { get; set; }

        public virtual DbSet<TblRecipientParentArchive> TblRecipientParentArchives { get; set; }

        public virtual DbSet<TblRegion> TblRegions { get; set; }

        public virtual DbSet<TblRole> TblRoles { get; set; }

        public virtual DbSet<TblSponsor> TblSponsors { get; set; }

        public virtual DbSet<TblStatusLog> TblStatusLogs { get; set; }

        public virtual DbSet<TblStatusLogArchive> TblStatusLogArchives { get; set; }

        public virtual DbSet<TblStatusType> TblStatusTypes { get; set; }

        public virtual DbSet<TblStory> TblStories { get; set; }

        public virtual DbSet<TblUser> TblUsers { get; set; }

        public virtual DbSet<TblViewHeader> TblViewHeaders { get; set; }

        public virtual DbSet<TblViewItemsAgency> TblViewItemsAgencies { get; set; }

        public virtual DbSet<TblViewItemsSponsor> TblViewItemsSponsors { get; set; }

        public virtual DbSet<TblViewItemsStatus> TblViewItemsStatuses { get; set; }

        public virtual DbSet<TblVolunteerLog> TblVolunteerLogs { get; set; }

        public virtual DbSet<TblVolunteerName> TblVolunteerNames { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationSetting>(entity =>
            {
                entity.HasKey(e => e.SettingsId);

                entity.Property(e => e.SettingsId).HasColumnName("SettingsID");
                entity.Property(e => e.Category).HasMaxLength(25);
                entity.Property(e => e.SettingsName).HasMaxLength(50);
                entity.Property(e => e.SettingsValue).HasMaxLength(100);
            });

            modelBuilder.Entity<GiftDetail>(entity =>
            {
                entity.ToTable("GiftDetail");

                entity.Property(e => e.GiftDetailText).HasMaxLength(100);
                entity.Property(e => e.GiftIdeaDescription).HasMaxLength(300);
                entity.Property(e => e.LblGiftDetail1)
                    .HasMaxLength(150)
                    .HasColumnName("lblGiftDetail1");
                entity.Property(e => e.LblGiftDetail2)
                    .HasMaxLength(150)
                    .HasColumnName("lblGiftDetail2");
            });

            modelBuilder.Entity<SecretSantaLog>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Table_1");

                entity.ToTable("SecretSantaLog");

                entity.Property(e => e.ExceptionMessage)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.ExceptionSource)
                    .HasMaxLength(200)
                    .IsUnicode(false);
                entity.Property(e => e.ExceptionType)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.LogDateTime).HasColumnType("datetime");
                entity.Property(e => e.StackTrace)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAdminEmail>(entity =>
            {
                entity.HasKey(e => e.EmailId).HasName("PK_SecretSantaLetters");

                entity.ToTable("tblAdminEmails");

                entity.Property(e => e.EmailContent).IsUnicode(false);
                entity.Property(e => e.EmailName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAgency>(entity =>
            {
                entity.HasKey(e => e.AgencyId);

                entity.ToTable("tblAgencies");

                entity.HasIndex(e => e.AgencyCode, "UC_tblAgencies_AgencyCode").IsUnique();

                entity.HasIndex(e => e.AgencyName, "UC_tblAgencies_AgencyName").IsUnique();

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
                entity.Property(e => e.AgencyCity).HasMaxLength(50);
                entity.Property(e => e.AgencyCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);
                entity.Property(e => e.AgencyName).HasMaxLength(75);
                entity.Property(e => e.AgencyState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.AgencyStreet).HasMaxLength(50);
                entity.Property(e => e.AgencyWebRank).HasDefaultValueSql("((0))");
                entity.Property(e => e.AgencyZip)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.Notes).HasMaxLength(255);
                entity.Property(e => e.NotificationLevel).HasDefaultValueSql("((10))");
                entity.Property(e => e.Overflow).HasDefaultValueSql("((0))");
                entity.Property(e => e.OverflowNotes).HasMaxLength(30);
                entity.Property(e => e.Payment).HasMaxLength(6);
                entity.Property(e => e.Region)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("(N'SR')")
                    .IsFixedLength();
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.Type)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Website).HasMaxLength(200);

                entity.HasOne(d => d.RegionNavigation).WithMany(p => p.TblAgencies)
                    .HasForeignKey(d => d.Region)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAgencies_tblRegions");
            });

            modelBuilder.Entity<TblAgencyCheckInLog>(entity =>
            {
                entity.HasKey(e => e.CheckInId).HasName("PK_tblCheckInLog");

                entity.ToTable("tblAgencyCheckInLog");

                entity.Property(e => e.CheckInId).HasColumnName("CheckInID");
                entity.Property(e => e.CheckInAgencyContactId)
                    .HasMaxLength(100)
                    .HasColumnName("CheckInAgencyContactID");
                entity.Property(e => e.CheckInDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Location).HasMaxLength(3);
            });

            modelBuilder.Entity<TblAgencyContact>(entity =>
            {
                entity.ToTable("tblAgencyContacts");

                entity.Property(e => e.Id).HasMaxLength(128);
                entity.Property(e => e.AltPhone)
                    .HasMaxLength(14)
                    .IsUnicode(false);
                entity.Property(e => e.Fax)
                    .HasMaxLength(14)
                    .IsUnicode(false);
                entity.Property(e => e.FirstName).HasMaxLength(30);
                entity.Property(e => e.LastName).HasMaxLength(30);
                entity.Property(e => e.Phone)
                    .HasMaxLength(14)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblAgencyLocation>(entity =>
            {
                entity.ToTable("tblAgencyLocations");

                entity.Property(e => e.TblAgencyLocationId).HasColumnName("tblAgencyLocationID");
                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
                entity.Property(e => e.LocDesc).HasMaxLength(50);
                entity.Property(e => e.Location).HasMaxLength(3);

                entity.HasOne(d => d.Agency).WithMany(p => p.TblAgencyLocations)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblAgencyLocations_tblAgencies");
            });

            modelBuilder.Entity<TblApp>(entity =>
            {
                entity.HasKey(e => e.AppId);

                entity.ToTable("tblApps");

                entity.Property(e => e.AppId).HasColumnName("AppID");
                entity.Property(e => e.AppName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.ChangeInfo).HasMaxLength(50);
                entity.Property(e => e.LaunchForm).HasMaxLength(50);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
            });

            modelBuilder.Entity<TblAppUser>(entity =>
            {
                entity.HasKey(e => e.AppUserAu);

                entity.ToTable("tblAppUsers");

                entity.Property(e => e.AppUserAu).HasColumnName("AppUserAU");
                entity.Property(e => e.AuappId).HasColumnName("AUAppID");
                entity.Property(e => e.Aupermission)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('User')")
                    .IsFixedLength()
                    .HasColumnName("AUPermission");
                entity.Property(e => e.Austatus)
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('Not Allowed')")
                    .IsFixedLength()
                    .HasColumnName("AUStatus");
                entity.Property(e => e.AuuserId).HasColumnName("AUUserID");
            });

            modelBuilder.Entity<TblCategoryDescription>(entity =>
            {
                entity.HasKey(e => e.CategoryId);

                entity.ToTable("tblCategoryDescriptions");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
                entity.Property(e => e.Category)
                    .HasMaxLength(9)
                    .IsUnicode(false);
                entity.Property(e => e.Description).HasMaxLength(150);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
            });

            modelBuilder.Entity<TblDonation>(entity =>
            {
                entity.HasKey(e => e.DonId);

                entity.ToTable("tblDonations");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.DonDateTime).HasColumnType("datetime");

                entity.HasOne(d => d.TblRecipientParent).WithMany(p => p.TblDonations)
                    .HasForeignKey(d => d.RecipientNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDonations_tblRecipientParent");
            });

            modelBuilder.Entity<TblDonationArchive>(entity =>
            {
                entity.ToTable("tblDonations_archive");
                entity.HasNoKey();
                entity.Property(e => e.DonNumber).HasMaxLength(40);
                entity.Property(e => e.DonAmount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.DonDateTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<TblDonor>(entity =>
            {
                entity.HasKey(e => e.DonorId);

                entity.ToTable("tblDonor");

                entity.Property(e => e.DonorId).HasMaxLength(128);
                entity.Property(e => e.DonorEmail).HasMaxLength(256);
            });

            modelBuilder.Entity<TblDonorThankYou>(entity =>
            {
                entity.HasKey(e => e.DonorThankYouId).HasName("PK_DonorThankYou");

                entity.ToTable("tblDonorThankYou");

                entity.Property(e => e.DonorId).HasMaxLength(128);
                entity.Property(e => e.Image).HasMaxLength(150);
                entity.Property(e => e.ThankYouDate).HasColumnType("datetime");

                entity.HasOne(d => d.Donor).WithMany(p => p.TblDonorThankYous)
                    .HasForeignKey(d => d.DonorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblDonorThankYou_tblDonor");
            });

            modelBuilder.Entity<TblElf>(entity =>
            {
                entity.HasKey(e => e.ElfId);

                entity.ToTable("tblElves");

                entity.Property(e => e.ElfId).HasColumnName("ElfID");
                entity.Property(e => e.ElfAltPhone)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("Elf_AltPhone");
                entity.Property(e => e.ElfEmail)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("Elf_Email");
                entity.Property(e => e.ElfFirstName)
                    .HasMaxLength(50)
                    .HasColumnName("Elf_FirstName");
                entity.Property(e => e.ElfLastName)
                    .HasMaxLength(50)
                    .HasColumnName("Elf_LastName");
                entity.Property(e => e.ElfPhone)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("Elf_Phone");
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
                entity.Property(e => e.Notes).HasMaxLength(250);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.TrainingDates)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblErrorLog>(entity =>
            {
                entity.HasKey(e => e.ErrorId);

                entity.ToTable("tblErrorLog");

                entity.Property(e => e.ErrorId).HasColumnName("ErrorID");
                entity.Property(e => e.AppId).HasColumnName("AppID");
                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.ErrorDate)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.ErrorDesc).HasMaxLength(500);
                entity.Property(e => e.Event).HasMaxLength(50);
                entity.Property(e => e.Field).HasMaxLength(50);
                entity.Property(e => e.Form).HasMaxLength(50);
                entity.Property(e => e.LabelId).HasColumnName("LabelID");
                entity.Property(e => e.Machine).HasMaxLength(30);
                entity.Property(e => e.MiscVariable).HasMaxLength(50);
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
            });

            modelBuilder.Entity<TblErrorLogArchive>(entity =>
            {
                entity.HasKey(e => e.ErrorId);

                entity.ToTable("tblErrorLog_archive");

                entity.Property(e => e.ErrorId).HasColumnName("ErrorID");
                entity.Property(e => e.AppId).HasColumnName("AppID");
                entity.Property(e => e.AppUserId).HasColumnName("AppUserID");
                entity.Property(e => e.Code).HasMaxLength(50);
                entity.Property(e => e.ErrorDate).HasColumnType("datetime");
                entity.Property(e => e.ErrorDesc).HasMaxLength(500);
                entity.Property(e => e.Event).HasMaxLength(50);
                entity.Property(e => e.Field).HasMaxLength(50);
                entity.Property(e => e.Form).HasMaxLength(50);
                entity.Property(e => e.LabelId).HasColumnName("LabelID");
                entity.Property(e => e.Machine).HasMaxLength(30);
                entity.Property(e => e.MiscVariable).HasMaxLength(50);
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblFamilyMember>(entity =>
            {
                entity.HasKey(e => e.FamilyMemberId);

                entity.ToTable("tblFamilyMember");

                entity.Property(e => e.Gender)
                    .HasMaxLength(2)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.AgeType).HasMaxLength(6);
                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.ShoeSize)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ShoeSizeType).HasMaxLength(15);
                entity.Property(e => e.WarmClothingSize)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.WarmClothingType).HasMaxLength(15);

                entity.HasOne(d => d.Letter).WithMany(p => p.TblFamilyMembers)
                    .HasForeignKey(d => d.LetterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblFamilyMember_tblLetterSanta");
            });

            modelBuilder.Entity<TblFamilyMemberArchive>(entity =>
            {
                entity.HasKey(e => e.FamilyMemberId);

                entity.ToTable("tblFamilyMember_archive");

                entity.Property(e => e.Gender)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(150);
                entity.Property(e => e.ShoeSize)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ShoeSizeType).HasMaxLength(15);
                entity.Property(e => e.WarmClothingSize)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.WarmClothingType).HasMaxLength(15);
                entity.Property(e => e.Year).HasMaxLength(4);
            });

            modelBuilder.Entity<TblHeartArchive>(entity =>
            {
                entity.ToTable("tblHeart_archive");
                entity.HasNoKey();
                entity.Property(e => e.AgeType).HasMaxLength(6);
                entity.Property(e => e.Gender).HasMaxLength(2);
                entity.Property(e => e.DonorId).HasMaxLength(128);
                entity.Property(e => e.Year).HasMaxLength(4);
                entity.Property(e => e.GiftCountInOut).HasMaxLength(2);
            });

            modelBuilder.Entity<TblLetterSanta>(entity =>
            {
                entity.HasKey(e => e.LetterId).HasName("pk_Santa");

                entity.ToTable("tblLetterSanta");

                entity.Property(e => e.Address).HasMaxLength(250);
                entity.Property(e => e.AdminGeneralNotes)
                    .HasMaxLength(100)
                    .IsFixedLength();
                entity.Property(e => e.AdminGeneralNotes1).HasMaxLength(100);
                entity.Property(e => e.AdminHistoryNotes).HasMaxLength(100);
                entity.Property(e => e.AdoptedBy).HasMaxLength(50);
                entity.Property(e => e.AdoptedByEmail).HasMaxLength(50);
                entity.Property(e => e.AdoptedByPhone)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.Agency).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.FamilyCode)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .IsUnicode(false);
                entity.Property(e => e.LetterReadyDate).HasColumnType("datetime");
                entity.Property(e => e.LetterSummary).HasMaxLength(100);
                entity.Property(e => e.Phone)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");
                entity.Property(e => e.PickupDate).HasColumnType("datetime");
                entity.Property(e => e.WriterName).HasMaxLength(200);
                entity.Property(e => e.Zip).HasMaxLength(10);
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblLetterSantaArchive>(entity =>
            {
                entity.HasKey(e => e.LetterId).HasName("pk_Santa_archive");

                entity.ToTable("tblLetterSanta_archive");

                entity.Property(e => e.twooneone).HasColumnName("211");
                entity.Property(e => e.Language).HasMaxLength(100);
                entity.Property(e => e.Address).HasMaxLength(250);
                entity.Property(e => e.AdminGeneralNotes).HasMaxLength(100).IsFixedLength();
                entity.Property(e => e.AdminGeneralNotes1).HasMaxLength(100);
                entity.Property(e => e.AdminHistoryNotes).HasMaxLength(100);
                entity.Property(e => e.AdoptedBy).HasMaxLength(50);
                entity.Property(e => e.AdoptedByEmail).HasMaxLength(50);
                entity.Property(e => e.AdoptedByPhone).HasMaxLength(15).IsUnicode(false);
                entity.Property(e => e.Agency).HasMaxLength(100);
                entity.Property(e => e.City).HasMaxLength(100);
                entity.Property(e => e.Email).HasMaxLength(256);
                entity.Property(e => e.FamilyCode).HasMaxLength(10).IsFixedLength();
                entity.Property(e => e.FirstName).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.LastName).HasMaxLength(100).IsUnicode(false);
                entity.Property(e => e.LetterReadyDate).HasColumnType("datetime");
                entity.Property(e => e.LetterSummary).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(12).IsUnicode(false).HasDefaultValueSql("('')");
                entity.Property(e => e.PickupDate).HasColumnType("datetime");
                entity.Property(e => e.WriterName).HasMaxLength(200);
                entity.Property(e => e.Zip).HasMaxLength(10);
                entity.Property(e => e.IsActive).IsRequired().HasDefaultValueSql("((1))");
                entity.Property(e => e.OtherHear).HasMaxLength(100);
                entity.Property(e => e.FirstNeedTypeId).HasMaxLength(50);
                entity.Property(e => e.SecondNeedTypeId).HasMaxLength(50);
                entity.Property(e => e.ThirdNeedTypeId).HasMaxLength(50);
                entity.Property(e => e.ForWho).HasMaxLength(25);
                entity.Property(e => e.SantaLetterCode).HasMaxLength(50);
                entity.Property(e => e.Year).HasMaxLength(4);
            });

            modelBuilder.Entity<TblLetterStatus>(entity =>
            {
                entity.HasKey(e => e.LetterStatusId);

                entity.ToTable("tblLetterStatus");

                entity.HasIndex(e => e.LetterId, "nci_wi_tblLetterStatus_431DE389AD2A0EADED389F4E4CDD4CE3");

                entity.Property(e => e.LetterStatusId).HasColumnName("LetterStatusID");
                entity.Property(e => e.DateEdited).HasColumnType("datetime");
                entity.Property(e => e.EditedByUser).HasMaxLength(128);
                entity.Property(e => e.LetterId).HasColumnName("LetterID");
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.StatusNote).HasMaxLength(100);

                entity.HasOne(d => d.Letter).WithMany(p => p.TblLetterStatuses)
                    .HasForeignKey(d => d.LetterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLetterStatus_tblLetterSanta");

                entity.HasOne(d => d.Status).WithMany(p => p.TblLetterStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLetterStatus_tblStatusTypes");
            });

            modelBuilder.Entity<TblLetterStatusArchive>(entity =>
            {
                entity.HasKey(e => e.LetterStatusId);

                entity.ToTable("tblLetterStatus_archive");

                entity.HasIndex(e => e.LetterId, "nci_wi_tblLetterStatus_431DE389AD2A0EADED389F4E4CDD4CE3");

                entity.Property(e => e.LetterStatusId).HasColumnName("LetterStatusID");
                entity.Property(e => e.DateEdited).HasColumnType("datetime");
                entity.Property(e => e.EditedByUser).HasMaxLength(128);
                entity.Property(e => e.LetterId).HasColumnName("LetterID");
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.StatusNote).HasMaxLength(100);

                entity.HasOne(d => d.Letter).WithMany(p => p.TblLetterStatusArchives)
                    .HasForeignKey(d => d.LetterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLetterStatus_tblLetterSanta_archive");

                entity.HasOne(d => d.Status).WithMany(p => p.TblLetterStatusArchives)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblLetterStatus_tblStatusTypes_archive");
            });

            modelBuilder.Entity<TblMessage>(entity =>
            {
                entity.HasKey(e => e.MessageId).HasName("PK_Message");

                entity.ToTable("tblMessage");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.MessageContent).IsUnicode(false);
                entity.Property(e => e.MessageTitle)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TblMessageContact>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.ContactId }).HasName("PK_MessageContact");

                entity.ToTable("tblMessageContact");

                entity.Property(e => e.ContactId).HasMaxLength(128);

                entity.HasOne(d => d.Contact).WithMany(p => p.TblMessageContacts)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMessageContact_tblAgencyContacts");

                entity.HasOne(d => d.Message).WithMany(p => p.TblMessageContacts)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblMessageContact_tblMessage");
            });

            modelBuilder.Entity<TblNeedType>(entity =>
            {
                entity.HasKey(e => e.NeedTypeId).HasName("PK__tblNeedT__18F2CFA5DC7621F9");

                entity.ToTable("tblNeedTypes");

                entity.Property(e => e.NeedTypeId).ValueGeneratedNever();
                entity.Property(e => e.NeedTypeString).HasMaxLength(50);
            });

            modelBuilder.Entity<TblRecipientChild>(entity =>
            {
                entity.HasKey(e => e.LabelNum);

                entity.ToTable("tblRecipientChild");
                entity.HasIndex(e => new { e.RecipientNum, e.Primary }, "nci_wi_tblRecipientChild_F676665CB29E6A64BB3F461258164B4E");
                entity.Property(e => e.Primary).HasDefaultValue(false);
                entity.Property(e => e.HistoryNotes).HasMaxLength(200);
                entity.Property(e => e.BulkEditTracker).HasMaxLength(10);
                entity.Property(e => e.BulkEditTracker2).HasMaxLength(10);
                entity.Property(e => e.RecChildFreefield).HasMaxLength(10);

                entity.HasOne(d => d.RecipientNumNavigation)
                    .WithMany(p => p.TblRecipientChildren)
                    .HasForeignKey(d => d.RecipientNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRecipientChild_tblRecipientParent");
            });

            modelBuilder.Entity<TblRecipientChildArchive>(entity =>
            {
                entity.HasKey(e => e.LabelNum);

                entity.ToTable("tblRecipientChild_archive");

                entity.Property(e => e.BulkEdit)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.BulkStatusChange)
                    .HasMaxLength(5)
                    .IsFixedLength();
                entity.Property(e => e.ConfirmHeartPrint)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.ConfirmTagPrint)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.EditNewRecipient)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.FreeField)
                    .HasMaxLength(5)
                    .IsFixedLength();
                entity.Property(e => e.HeartCentralGiftCode)
                    .HasMaxLength(1)
                    .IsFixedLength();
                entity.Property(e => e.HistoryNotes).HasMaxLength(200);
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
                entity.Property(e => e.PrintHeart)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.PrintTag)
                    .HasMaxLength(10)
                    .IsFixedLength();
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.RecipientNumNavigation).WithMany(p => p.TblRecipientChildArchives)
                    .HasForeignKey(d => d.RecipientNum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblRecipientChild_tblRecipientParent_archive");
            });

            modelBuilder.Entity<TblRecipientParent>(entity =>
            {
                entity.HasKey(e => e.RecipientNum);

                entity.ToTable("tblRecipientParent");

                entity.HasIndex(e => new { e.DonorId, e.IsActive }, "nci_wi_tblRecipientParent_44C63202CD904BBFF53DE74223D6A6B7");

                entity.HasIndex(e => e.AgencyId, "nci_wi_tblRecipientParent_8DB3CC20934C2198DA4B4BF14E2A8383");

                entity.Property(e => e.Name)
                    .HasMaxLength(25);

                entity.Property(e => e.AgeType)
                    .HasMaxLength(6)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('years')");

                entity.Property(e => e.Gender)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.RecipientInfo)
                    .HasMaxLength(35)
                    .IsUnicode(false);

                entity.Property(e => e.GiftWish)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.GiftDetail1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.GiftDetail2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AltGiftWish)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.AltGiftDetail1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AltGiftDetail2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.AgencyId)
                    .HasColumnName("AgencyID");

                entity.Property(e => e.Resubmit)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.EditNotes)
                    .HasMaxLength(500)
                    .IsRequired(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(4);

                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.DonorId)
                    .HasMaxLength(128)
                    .IsRequired(false);

                entity.Property(e => e.DonorRegisterDate)
                    .HasColumnType("datetime")
                    .IsRequired(false);

                entity.Property(e => e.DateEntered)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.Property(e => e.WebRank)
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FloorGiftCount)
                    .IsRequired(false);

                entity.Property(e => e.GiftCardCount)
                    .IsRequired(false);

                entity.Property(e => e.BikeCount)
                    .IsRequired(false);

                entity.Property(e => e.TotalGiftCount)
                    .IsRequired(false);

                entity.Property(e => e.PrintTracker)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity.Property(e => e.ApproveTracker)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired(false);

                entity.Property(e => e.RecParFreefield)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsRequired(false);


                entity.HasOne(d => d.Agency).WithMany(p => p.TblRecipientParents)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("FK_tblRecipientParent_tblAgencies");

                entity.HasOne(d => d.AltGiftTypeNavigation).WithMany(p => p.TblRecipientParentAltGiftTypeNavigations)
                    .HasForeignKey(d => d.AltGiftType)
                    .HasConstraintName("FK_tblRecipientParent_AltGiftDetail");

                entity.HasOne(d => d.Donor).WithMany(p => p.TblRecipientParents)
                    .HasForeignKey(d => d.DonorId)
                    .HasConstraintName("FK_tblRecipientParent_tblDonor");

                entity.HasOne(d => d.GiftTypeNavigation).WithMany(p => p.TblRecipientParentGiftTypeNavigations)
                    .HasForeignKey(d => d.GiftType)
                    .HasConstraintName("FK_tblRecipientParent_GiftDetail");
            });

            modelBuilder.Entity<TblRecipientParentArchive>(entity =>
            {
                entity.HasKey(e => e.RecipientNum);

                entity.ToTable("tblRecipientParent_archive");

                entity.Property(e => e.AgeType)
                    .HasMaxLength(6)
                    .IsUnicode(false);
                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
                entity.Property(e => e.AltGiftDetail1)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.AltGiftDetail2)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.AltGiftWish)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.DateEntered)
                    .HasDefaultValueSql("('11/1/2019')")
                    .HasColumnType("datetime");
                entity.Property(e => e.DonorId).HasMaxLength(128);
                entity.Property(e => e.DonorRegisterDate).HasColumnType("datetime");
                entity.Property(e => e.EditNotes).HasMaxLength(500);
                entity.Property(e => e.Freefield)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.Gender)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.GiftDetail1)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.GiftDetail2)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.GiftWish)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
                entity.Property(e => e.Location).HasMaxLength(3);
                entity.Property(e => e.Name).HasMaxLength(25);
                entity.Property(e => e.RecipientInfo)
                    .HasMaxLength(35)
                    .IsUnicode(false);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.SponsorId).HasColumnName("SponsorID");
                entity.Property(e => e.ThankYouRecieved).HasDefaultValueSql("((0))");
                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Agency).WithMany(p => p.TblRecipientParentArchives)
                    .HasForeignKey(d => d.AgencyId)
                    .HasConstraintName("FK_tblRecipientParent_tblAgencies_archive");

                entity.HasOne(d => d.AltGiftTypeNavigation).WithMany(p => p.TblRecipientParentArchiveAltGiftTypeNavigations)
                    .HasForeignKey(d => d.AltGiftType)
                    .HasConstraintName("FK_tblRecipientParent_AltGiftDetail_archive");

                entity.HasOne(d => d.Donor).WithMany(p => p.TblRecipientParentArchives)
                    .HasForeignKey(d => d.DonorId)
                    .HasConstraintName("FK_tblRecipientParent_tblDonor_archive");

                entity.HasOne(d => d.GiftTypeNavigation).WithMany(p => p.TblRecipientParentArchiveGiftTypeNavigations)
                    .HasForeignKey(d => d.GiftType)
                    .HasConstraintName("FK_tblRecipientParent_GiftDetail_archive");

                entity.HasOne(d => d.Sponsor).WithMany(p => p.TblRecipientParentArchives)
                    .HasForeignKey(d => d.SponsorId)
                    .HasConstraintName("FK_tblRecipientParent_tblSponsors_archive");
            });

            modelBuilder.Entity<TblRegion>(entity =>
            {
                entity.HasKey(e => e.RegionShort);

                entity.ToTable("tblRegions");

                entity.HasIndex(e => e.RegionShort, "IX_tblRegions_1").IsUnique();

                entity.Property(e => e.RegionShort)
                    .HasMaxLength(4)
                    .IsFixedLength();
                entity.Property(e => e.RegionLong)
                    .HasMaxLength(30)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblRole>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.ToTable("tblRoles");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");
                entity.Property(e => e.RoleName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
            });

            modelBuilder.Entity<TblSponsor>(entity =>
            {
                entity.HasKey(e => e.SponsorId);

                entity.ToTable("tblSponsors");

                entity.HasIndex(e => e.ElfID, "NonClusteredIndex-tblSponsors-ElfID");

                entity.Property(e => e.SponsorId).HasColumnName("SponsorID");
                entity.Property(e => e.AdoptAHeartDisplay).HasColumnName("AdoptAHeartDisplay");
                entity.Property(e => e.DeliveryDate)
                    .HasMaxLength(15)
                    .IsUnicode(false);
                entity.Property(e => e.DisplaySignage)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.DisplayType)
                    .HasMaxLength(25)
                    .IsUnicode(false);
                entity.Property(e => e.ElfID).HasColumnName("ElfID");
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.LYGiftAssigned)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("LYGiftAssigned");
                entity.Property(e => e.LYGiftOut)
                    .HasMaxLength(10)
                    .IsFixedLength()
                    .HasColumnName("LYGiftOut");
                entity.Property(e => e.Notes).HasMaxLength(250);
                entity.Property(e => e.PrimaryEmail)
                    .HasMaxLength(75)
                    .IsUnicode(false);
                entity.Property(e => e.PrimaryFirstName).HasMaxLength(30);
                entity.Property(e => e.PrimaryLastName).HasMaxLength(30);
                entity.Property(e => e.PrimaryPhone)
                    .HasMaxLength(14)
                    .IsUnicode(false);
                entity.Property(e => e.Region)
                    .HasMaxLength(4)
                    .HasDefaultValueSql("(N'SR')")
                    .IsFixedLength();
                entity.Property(e => e.Row_Version)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.SongRequest)
                    .HasMaxLength(50)
                    .IsUnicode(false);
                entity.Property(e => e.SponsorCity)
                    .HasMaxLength(30)
                    .IsUnicode(false);
                entity.Property(e => e.SponsorName).HasMaxLength(50);
                entity.Property(e => e.SponsorState)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();
                entity.Property(e => e.SponsorStreet).HasMaxLength(30);
                entity.Property(e => e.SponsorZip)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.SS_AltPhone)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("SS_AltPhone");
                entity.Property(e => e.SS_Email)
                    .HasMaxLength(75)
                    .IsUnicode(false)
                    .HasColumnName("SS_Email");
                entity.Property(e => e.SS_FirstName)
                    .HasMaxLength(30)
                    .HasColumnName("SS_FirstName");
                entity.Property(e => e.SS_LastName)
                    .HasMaxLength(30)
                    .HasColumnName("SS_LastName");
                entity.Property(e => e.SS_Phone)
                    .HasMaxLength(14)
                    .IsUnicode(false)
                    .HasColumnName("SS_Phone");

                entity.Property(e => e.OpenToPublic)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Elf).WithMany(p => p.TblSponsors)
                    .HasForeignKey(d => d.ElfID)
                    .HasConstraintName("FK_tblSponsors_tblElves");

                entity.HasOne(d => d.RegionNavigation).WithMany(p => p.TblSponsors)
                    .HasForeignKey(d => d.Region)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblSponsors_tblRegions");
            });

            modelBuilder.Entity<TblStatusLog>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("tblStatusLog");

                entity.HasIndex(e => e.LabelNum, "nci_wi_tblStatusLog_CACBFEF9C020D3B63A46A19646FD8908");

                entity.HasIndex(e => new { e.StatusId }, "nci_wi_tblStatusLog_F8759D1A6B87A674B38E3C8B5FA96258");

                entity.Property(e => e.LabelNum)
                    .HasColumnName("LabelNum")
                    .HasColumnType("int");

                entity.Property(e => e.LogId)
                    .HasColumnName("LogID");

                entity.Property(e => e.AppId)
                    .HasColumnName("AppID");

                entity.Property(e => e.ChangeInfo)
                    .HasMaxLength(500);

                entity.Property(e => e.DateEdited)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");

                entity.Property(e => e.StatusId)
                    .HasColumnName("StatusID");

                entity.HasOne(d => d.App).WithMany(p => p.TblStatusLogs)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatusLog_tblApps");

                entity.HasOne(d => d.LabelNumNavigation)
                    .WithMany(p => p.TblStatusLogs)
                    .HasForeignKey(d => d.LabelNum)
                    .HasConstraintName("FK_tblStatusLog_tblRecipientChild");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblStatusLogs)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatusLog_tblStatusTypes");
            });

            modelBuilder.Entity<TblStatusLogArchive>(entity =>
            {
                entity.HasKey(e => e.LogId);

                entity.ToTable("tblStatusLog_archive");

                entity.Property(e => e.LogId).HasColumnName("LogID");
                entity.Property(e => e.AppId).HasColumnName("AppID");
                entity.Property(e => e.ChangeInfo).HasMaxLength(500);
                entity.Property(e => e.DateEdited).HasColumnType("datetime");
                entity.Property(e => e.EditedByAppDate).HasColumnType("datetime");
                entity.Property(e => e.EditedByUser).HasMaxLength(128);
                entity.Property(e => e.Machine).HasMaxLength(30);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.Year)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.App).WithMany(p => p.TblStatusLogArchives)
                    .HasForeignKey(d => d.AppId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatusLog_tblApps_archive");

                entity.HasOne(d => d.EditedByAppUserNavigation)
                    .WithMany(p => p.TblStatusLogArchives)
                    .HasForeignKey(d => d.EditedByAppUser)
                    .HasConstraintName("FK_tblStatusLog_tblUsers_archive");

                entity.HasOne(d => d.LabelNumNavigation).WithMany(p => p.TblStatusLogArchives)
                    .HasForeignKey(d => d.LabelNum)
                    .HasConstraintName("FK_tblStatusLog_tblRecipientChild_archive");

                entity.HasOne(d => d.Status).WithMany(p => p.TblStatusLogArchives)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblStatusLog_tblStatusTypes_archive");
            });

            modelBuilder.Entity<TblStatusType>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("tblStatusTypes");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.ChartGroup1).HasMaxLength(8);
                entity.Property(e => e.ChartGroup2).HasMaxLength(8);
                entity.Property(e => e.Letters).HasDefaultValueSql("((0))");
                entity.Property(e => e.RecipientChild).HasDefaultValueSql("((0))");
                entity.Property(e => e.RecipientWebGroup).HasMaxLength(10);
                entity.Property(e => e.RowVersion)
                    .IsRowVersion()
                    .IsConcurrencyToken()
                    .HasColumnName("Row_Version");
                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .IsUnicode(false);
                entity.Property(e => e.StatusDescription)
                    .HasMaxLength(255)
                    .IsUnicode(false);
                entity.Property(e => e.WebGroup).HasMaxLength(30);
            });

            modelBuilder.Entity<TblStory>(entity =>
            {
                entity.HasKey(e => e.StoryId).HasName("PK_StoryIUd");

                entity.ToTable("tblStory");

                entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
                entity.Property(e => e.Title).HasMaxLength(150);
                entity.Property(e => e.IsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK_tblAppUserTest");

                entity.ToTable("tblUsers");

                entity.Property(e => e.UserId).HasColumnName("UserID");
                entity.Property(e => e.CellPhone).HasMaxLength(50);
                entity.Property(e => e.Email).HasMaxLength(150);
                entity.Property(e => e.UserDateCreated)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UserFirstName)
                    .HasMaxLength(30)
                    .IsFixedLength();
                entity.Property(e => e.UserIsActive)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
                entity.Property(e => e.UserLastName)
                    .HasMaxLength(50)
                    .IsFixedLength();
                entity.Property(e => e.UserMessage).HasDefaultValueSql("((1))");
                entity.Property(e => e.UserName)
                    .HasMaxLength(100)
                    .IsFixedLength();
                entity.Property(e => e.UserPassword)
                    .HasMaxLength(25)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TblViewHeader>(entity =>
            {
                entity.HasKey(e => e.ViewId);

                entity.ToTable("tblViewHeaders");

                entity.HasIndex(e => e.ViewDescription, "IX_tblViewHeaders").IsUnique();

                entity.Property(e => e.ViewId).HasColumnName("ViewID");
                entity.Property(e => e.AgeFromSearch).HasColumnName("AgeFrom_Search");
                entity.Property(e => e.AgeToSearch).HasColumnName("AgeTo_Search");
                entity.Property(e => e.AndOrMode)
                    .HasMaxLength(3)
                    .IsFixedLength();
                entity.Property(e => e.BigItemSearch)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasColumnName("BigItem_Search");
                entity.Property(e => e.BikeSearch)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasColumnName("Bike_Search");
                entity.Property(e => e.GenderSearch)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasColumnName("Gender_Search");
                entity.Property(e => e.GiftCardSearch)
                    .HasMaxLength(1)
                    .IsFixedLength()
                    .HasColumnName("GiftCard_Search");
                entity.Property(e => e.GiftDescription1Search)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("GiftDescription1_Search");
                entity.Property(e => e.GiftDescription2Search)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("GiftDescription2_Search");
                entity.Property(e => e.GiftTypeSearch)
                    .HasMaxLength(20)
                    .IsFixedLength()
                    .HasColumnName("GiftType_Search");
                entity.Property(e => e.ViewCreaterUserId).HasColumnName("ViewCreater_UserID");
                entity.Property(e => e.ViewDescription)
                    .HasMaxLength(75)
                    .IsFixedLength();

                entity.HasOne(d => d.ViewCreaterUser).WithMany(p => p.TblViewHeaders)
                    .HasForeignKey(d => d.ViewCreaterUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewHeaders_tblUsers");
            });

            modelBuilder.Entity<TblViewItemsAgency>(entity =>
            {
                entity.HasKey(e => e.ViewAgencyKey).HasName("PK_tblViewItemsAgency");

                entity.ToTable("tblViewItems_Agency");

                entity.Property(e => e.AgencyId).HasColumnName("AgencyID");
                entity.Property(e => e.ViewId).HasColumnName("ViewID");

                entity.HasOne(d => d.Agency).WithMany(p => p.TblViewItemsAgencies)
                    .HasForeignKey(d => d.AgencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewItems_Agency_tblAgencies");

                entity.HasOne(d => d.View).WithMany(p => p.TblViewItemsAgencies)
                    .HasForeignKey(d => d.ViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewItems_Agency_tblViewHeaders");
            });

            modelBuilder.Entity<TblViewItemsSponsor>(entity =>
            {
                entity.HasKey(e => e.ViewSponsorKey);

                entity.ToTable("tblViewItems_Sponsor");

                entity.Property(e => e.SponsorId).HasColumnName("SponsorID");
                entity.Property(e => e.ViewId).HasColumnName("ViewID");

                entity.HasOne(d => d.Sponsor).WithMany(p => p.TblViewItemsSponsors)
                    .HasForeignKey(d => d.SponsorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewItems_Sponsor_tblSponsors");

                entity.HasOne(d => d.View).WithMany(p => p.TblViewItemsSponsors)
                    .HasForeignKey(d => d.ViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewItems_Sponsor_tblViewHeaders");
            });

            modelBuilder.Entity<TblViewItemsStatus>(entity =>
            {
                entity.HasKey(e => e.ViewStatusKey);

                entity.ToTable("tblViewItems_Status");

                entity.Property(e => e.StatusId).HasColumnName("StatusID");
                entity.Property(e => e.ViewId).HasColumnName("ViewID");

                entity.HasOne(d => d.Status).WithMany(p => p.TblViewItemsStatuses)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewItems_Status_tblStatusTypes");

                entity.HasOne(d => d.View).WithMany(p => p.TblViewItemsStatuses)
                    .HasForeignKey(d => d.ViewId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tblViewItems_Status_tblViewHeaders");
            });

            modelBuilder.Entity<TblVolunteerLog>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("tblVolunteerLog");

                entity.Property(e => e.Company).HasMaxLength(50);
                entity.Property(e => e.FreeField).HasMaxLength(500);
                entity.Property(e => e.Site).HasMaxLength(50);
                entity.Property(e => e.TimeIn).HasColumnType("datetime");
                entity.Property(e => e.TimeOut).HasColumnType("datetime");
                entity.Property(e => e.VolId).HasColumnName("VolID");
                entity.Property(e => e.VolLogNum).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<TblVolunteerName>(entity =>
            {
                entity.HasKey(e => e.VolId);

                entity.ToTable("tblVolunteerNames");

                entity.Property(e => e.VolId).HasColumnName("VolID");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.FirstName).HasMaxLength(50);
                entity.Property(e => e.FreeField).HasMaxLength(200);
                entity.Property(e => e.LastName).HasMaxLength(50);
            });


            base.OnModelCreating(modelBuilder);
        }
    }
}