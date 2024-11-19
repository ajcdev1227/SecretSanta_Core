using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Models;
using System.Data;

namespace SecretSanta_Core.Repositories
{
    public class ThankYouRepository : GenericRepository<ThankYouRecipientModel>
    {
        public ThankYouRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }

        protected override ThankYouRecipientModel PopulateRecord(SqlDataReader reader)
        {
            var recipient = new ThankYouRecipientModel();
            recipient.RecipientNum = Convert.ToInt32(reader["RecipientNum"]);
            recipient.Name = reader["Name"].ToString();
            recipient.Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : (int?)null;
            recipient.AgeType = reader["AgeType"] != DBNull.Value ? reader["AgeType"].ToString() : null;
            recipient.Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null;
            recipient.RecipientInfo = reader["RecipientInfo"] != DBNull.Value ? reader["RecipientInfo"].ToString() : null;
            recipient.GiftWish = reader["GiftWish"] != DBNull.Value ? reader["GiftWish"].ToString() : null;
            recipient.AltGiftWish = reader["AltGiftWish"] != DBNull.Value ? reader["AltGiftWish"].ToString() : null;

            return recipient;
        }

        public List<ThankYouRecipientModel> GetThankYouForAgency(int agencyId)
        {
            var command = new SqlCommand
            {
                CommandText =
                    "SELECT p.RecipientNum, p.Name, p.Age, p.AgeType, p.Gender, p.RecipientInfo, p.GiftWish, p.AltGiftWish " +
                    "FROM tblRecipientParent p INNER JOIN tblRecipientChild c ON p.RecipientNum = c.RecipientNum " +
                    "INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum " +
                    "LEFT OUTER JOIN tblDonorThankYou on p.RecipientNum = tblDonorThankYou.RecipientNum " +
                    "INNER JOIN tblStatusTypes a ON a.StatusID = l.StatusID " +
                    "Inner Join (Select LabelNum, Max(LogID) maxId from tblStatusLog group by LabelNum) b " +
                    "on l.LabelNum = b.LabelNum and l.LogID = b.maxId " +
                    "WHERE AgencyID =@AgencyId " +
                    "AND a.WebGroup in ('Out','OutGfCard', 'OutBike') " +
                    "AND p.DonorId is not null " +
                    "group by p.RecipientNum, p.Name, p.Age, p.AgeType, p.Gender, p.RecipientInfo, p.GiftWish, p.AltGiftWish",

                    CommandType = CommandType.Text
            };
            command.Parameters.AddWithValue("AgencyId", agencyId);
            return GetRecords(command);
        }
    }
}
