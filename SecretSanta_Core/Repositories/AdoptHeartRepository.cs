using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Models;
using System.Data;
using System.Text;
using SecretSanta_Core.BusinessLogic;

namespace SecretSanta_Core.Repositories
{
    public class AdoptHeartRepository : GenericRepository<AdoptAHeartModel>
    {
        public AdoptHeartRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }
        protected override AdoptAHeartModel PopulateRecord(SqlDataReader reader)
        {
            var heart = new AdoptAHeartModel();
            heart.RecipientNumber = Convert.ToInt32(reader["RecipientNum"]);
            heart.LabelNum = Convert.ToInt32(reader["LabelNum"]);
            heart.Name = reader["Name"].GetSafeString();
            heart.Gender = reader["Gender"].GetSafeString();
            heart.Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : -1;
            heart.AgeType = reader["AgeType"] != DBNull.Value ? reader["AgeType"].ToString() : "years";
            heart.RecipientInfo = reader["RecipientInfo"].GetSafeString();
            heart.GiftType = reader["GiftType"].GetSafeInt();
            heart.AltGiftType = reader["AltGiftType"].GetSafeInt();
            heart.FirstWish = reader["GiftWish"].GetSafeString();
            heart.SecondWish = reader["AltGiftWish"].GetSafeString();
            heart.GiftDetail1 = reader["GiftDetail1"].GetSafeString();
            heart.GiftDetail2 = reader["GiftDetail2"].GetSafeString();
            heart.AltGiftDetail1 = reader["AltGiftDetail1"].GetSafeString();
            heart.AltGiftDetail2 = reader["AltGiftDetail2"].GetSafeString();
            heart.Location = reader["Location"].GetSafeString();
            heart.AgencyCode = reader["AgencyCode"].GetSafeString();
            heart.DateEntered = reader["DateEntered"] != DBNull.Value ? Convert.ToDateTime(reader["DateEntered"]) : new DateTime(2020, 1, 1);
            heart.WebRank = reader["WebRank"].GetSafeInt();

            return heart;
        }

        public List<AdoptAHeartModel> GetHearts()
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT tblRecipientParent.*, tblRecipientChild.LabelNum, tblAgencies.AgencyCode ");
            sb.AppendLine("FROM tblRecipientParent INNER JOIN tblRecipientChild ");
            sb.AppendLine("ON tblRecipientParent.RecipientNum = tblRecipientChild.RecipientNum ");
            sb.AppendLine("INNER JOIN tblAgencies ON tblAgencies.AgencyID = tblRecipientParent.AgencyID ");
            sb.AppendLine("WHERE tblRecipientParent.DonorId IS NULL ");
            sb.AppendLine("AND tblRecipientParent.IsActive = 1 ");
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecords(command);
        }

        public AdoptAHeartModel GetHeart(int recipientNum)
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT tblRecipientParent.*, tblRecipientChild.LabelNum, tblAgencies.AgencyCode ");
            sb.AppendLine("FROM tblRecipientParent INNER JOIN tblRecipientChild ");
            sb.AppendLine("ON tblRecipientParent.RecipientNum = tblRecipientChild.RecipientNum ");
            sb.AppendLine("INNER JOIN tblAgencies ON tblAgencies.AgencyID = tblRecipientParent.AgencyID ");
            sb.AppendLine("WHERE tblRecipientParent.RecipientNum = @RecipientNum");
            command.Parameters.AddWithValue("RecipientNum", recipientNum);
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecord(command);
        }

        public List<AdoptAHeartModel> GetHeartsForDonor(string donorId)
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT p.RecipientNum, p.Name, p.Gender, p.Age, p.AgeType,p.RecipientInfo, p.GiftType, p.AltGiftType, p.GiftWish, p.AltGiftWish, p.GiftDetail1, p.GiftDetail2, ");
            sb.AppendLine("p.AltGiftDetail1, p.AltGiftDetail2,p.Location, p.DateEntered,p.WebRank, ");
            sb.AppendLine("tblRecipientChild.LabelNum, tblAgencies.AgencyCode ");
            sb.AppendLine("FROM tblRecipientParent p INNER JOIN tblRecipientChild  ");
            sb.AppendLine("ON p.RecipientNum = tblRecipientChild.RecipientNum  ");
            sb.AppendLine("INNER JOIN tblAgencies ON tblAgencies.AgencyID = p.AgencyID  ");
            sb.AppendLine("WHERE p.DonorId = @DonorId AND tblRecipientChild.[Primary] = 1 ");
            sb.AppendLine("UNION ");
            sb.AppendLine("SELECT pa.RecipientNum, pa.Name, pa.Gender, pa.Age, pa.AgeType,pa.RecipientInfo, pa.GiftType, pa.AltGiftType, pa.GiftWish, pa.AltGiftWish, pa.GiftDetail1, pa.GiftDetail2, ");
            sb.AppendLine("pa.AltGiftDetail1, pa.AltGiftDetail2,pa.Location, pa.DateEntered,pa.WebRank, ");
            sb.AppendLine("tblRecipientChild_archive.LabelNum, tblAgencies.AgencyCode ");
            sb.AppendLine("FROM tblRecipientParent_archive pa INNER JOIN tblRecipientChild_archive ");
            sb.AppendLine("ON pa.RecipientNum = tblRecipientChild_archive.RecipientNum ");
            sb.AppendLine("INNER JOIN tblAgencies ON tblAgencies.AgencyID = pa.AgencyID  ");
            sb.AppendLine("WHERE pa.DonorId = @DonorId AND tblRecipientChild_archive.[Primary] = 1 ");
            command.Parameters.AddWithValue("DonorId", donorId);
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecords(command);
        }


        public string GetStatus(int recipientNumber)
        {

            var commandText =
                "SELECT a.Status " +
                "FROM tblRecipientParent r " +
                "INNER JOIN tblRecipientChild c ON r.RecipientNum = c.RecipientNum " +
                "INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum " +
                "INNER JOIN tblStatusTypes a ON a.StatusID = l.StatusID " +
                "Inner Join (SELECT LabelNum, Max(LogID) maxId FROM tblStatusLog group by LabelNum) b " +
                "ON l.LabelNum = b.LabelNum AND l.LogID = b.maxId " +
                "WHERE r.RecipientNum=@RecipientNum AND c.[Primary] = 1";

            var command = new SqlCommand
            {
                CommandText = commandText,
                CommandType = CommandType.Text
            };
            command.Parameters.AddWithValue("RecipientNum", recipientNumber);
            return ExecuteScalarForString(command);
        }
    }
}
