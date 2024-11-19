using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Enumerations;
using SecretSanta_Core.Models;
using System.Data;
using System.Text;

namespace SecretSanta_Core.Repositories
{
    public class RecipientViewRepository : GenericRepository<RecipientViewModel>
    {
		public static readonly string[] DashboardTypeStrings =
		{
			"Draft",
			"New",
			"Approved",
			"Revise",
			"Active",
			"Cancel",
			"WebAvail",
			"WebAdopt",
			"FLOOR GIFTS IN",
			"BIKES IN",
			"GIFT CARDS IN",
			"Out",
			"OutBike",
			"OutGfCard",
			"Other",
			"Pending"
		};
		public RecipientViewRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }

        protected override RecipientViewModel PopulateRecord(SqlDataReader reader)
        {
            var recipient = new RecipientViewModel();
            recipient.RecipientNum = Convert.ToInt32(reader["RecipientNum"]);
            recipient.Name = reader["Name"].ToString();
            recipient.Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : (int?)null;
            recipient.AgeType = reader["AgeType"] != DBNull.Value ? reader["AgeType"].ToString() : null;
            recipient.Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null;
            recipient.RecipientInfo = reader["RecipientInfo"] != DBNull.Value ? reader["RecipientInfo"].ToString() : null;
            recipient.GiftWish = reader["GiftWish"] != DBNull.Value ? reader["GiftWish"].ToString() : null;
            recipient.AltGiftWish = reader["AltGiftWish"] != DBNull.Value ? reader["AltGiftWish"].ToString() : null;
            recipient.EditNotes = reader["EditNotes"] != DBNull.Value ? reader["EditNotes"].ToString() : null;
            recipient.Status = reader["Status"].ToString();

            return recipient;
        }

        public List<RecipientViewModel> GetRecipientsByStatus(List<int> webGroups, int agencyId)
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT tblRecipientParent.*, a.Status FROM tblRecipientParent INNER JOIN tblRecipientChild c ON ");
            sb.AppendLine("tblRecipientParent.RecipientNum = c.RecipientNum ");
            sb.AppendLine("INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum ");
            sb.AppendLine("INNER JOIN tblStatusTypes a ON l.StatusID = a.StatusID ");
            sb.AppendLine("Inner Join (SELECT LabelNum, Max(LogID) maxId FROM tblStatusLog group by LabelNum) b ");
            sb.AppendLine("ON l.LabelNum = b.LabelNum and l.LogID = b.maxId");
            sb.AppendLine("WHERE tblRecipientParent.AgencyID=@AgencyId AND c.[Primary] = 1");

            DashboardTypes e = DashboardTypes.Draft;
            sb.Append(" AND (");
            var c = String.Empty;
            for (var i = 0; i < webGroups.Count; i++)
            {
                if (webGroups[i] == -1)
                {
                    sb.AppendLine(c + "a.RecipientWebGroup != 'active'");
                }
                else
                {
					sb.AppendLine(c + "a.WebGroup = '" + DashboardTypeStrings[webGroups[i]] + "' ");
				}
                
                c = " OR ";
            }

            sb.Append(")");
            command.Parameters.AddWithValue("AgencyId", agencyId);
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecords(command);
        }

        public List<RecipientViewModel> GetRecipientsForAgency(int agencyId)
        {
            var command = new SqlCommand
            {
                CommandText =
                    "SELECT r.RecipientNum, r.Name, r.age, r.AgeType, r.Gender, r.RecipientInfo, " +
                    "r.giftwish, r.Altgiftwish, r.editNotes, a.Status " +
                    "FROM tblRecipientParent r " +
                    "INNER JOIN tblRecipientChild c ON r.RecipientNum = c.RecipientNum " +
                    "INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum " +
                    "INNER JOIN tblStatusTypes a ON a.StatusID = l.StatusID " +
                    "Inner Join (SELECT LabelNum, Max(LogID) maxId FROM tblStatusLog group by LabelNum) b " +
                    "ON l.LabelNum = b.LabelNum and l.LogID = b.maxId " +
                    "WHERE AgencyID =@AgencyId AND c.[Primary] = 1",

                CommandType = CommandType.Text
            };
            command.Parameters.AddWithValue("AgencyId", agencyId);
            return GetRecords(command);
        }

    }
}
