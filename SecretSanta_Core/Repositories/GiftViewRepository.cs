using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Enumerations;
using SecretSanta_Core.Models;
using System.Data;
using System.Text;

namespace SecretSanta_Core.Repositories
{
	public class GiftViewRepository : GenericRepository<GiftViewModel>
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
		public GiftViewRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
		{

		}

		protected override GiftViewModel PopulateRecord(SqlDataReader reader)
		{
			var gift = new GiftViewModel();
			gift.LabelNum = Convert.ToInt32(reader["LabelNum"]);
			gift.RecipientNum = Convert.ToInt32(reader["RecipientNum"]);
			gift.Name = reader["Name"].ToString();
			gift.Age = reader["Age"] != DBNull.Value ? Convert.ToInt32(reader["Age"]) : (int?)null;
			gift.AgeType = reader["AgeType"] != DBNull.Value ? reader["AgeType"].ToString() : null;
			gift.Gender = reader["Gender"] != DBNull.Value ? reader["Gender"].ToString() : null;
			gift.RecipientInfo = reader["RecipientInfo"] != DBNull.Value ? reader["RecipientInfo"].ToString() : null;
			gift.GiftWish = reader["GiftWish"] != DBNull.Value ? reader["GiftWish"].ToString() : null;
			gift.AltGiftWish = reader["AltGiftWish"] != DBNull.Value ? reader["AltGiftWish"].ToString() : null;
			gift.EditNotes = reader["EditNotes"] != DBNull.Value ? reader["EditNotes"].ToString() : null;
			gift.Status = reader["Status"].ToString();
			gift.Location = reader["Location"].ToString();

			return gift;
		}


		public List<GiftViewModel> GetGiftsForAgency(int agencyId)
		{
			var command = new SqlCommand
			{
				CommandText =
					"SELECT tblRecipientParent.*, c.LabelNum, a.Status FROM tblRecipientParent " +
					"INNER JOIN tblRecipientChild c ON tblRecipientParent.RecipientNum = c.RecipientNum " +
					"INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum " +
					"INNER JOIN tblStatusTypes a ON a.StatusID = l.StatusID " +
					//"LEFT OUTER JOIN tblAgencyLocations locations on locations.Location = tblRecipientParent.Location " +
					"Inner Join (SELECT LabelNum, Max(LogID) maxId from tblStatusLog group by LabelNum) b " +
					"on l.LabelNum = b.LabelNum and l.LogID = b.maxId " +
					"WHERE tblRecipientParent.AgencyID =@AgencyId",

				CommandType = CommandType.Text
			};
			command.Parameters.AddWithValue("AgencyId", agencyId);
			return GetRecords(command);
		}

		public List<GiftViewModel> GetGiftsByStatus(List<int> webGroups, int agencyId)
		{
			var command = new SqlCommand();
			var sb = new StringBuilder();
			sb.AppendLine("SELECT tblRecipientParent.*, c.LabelNum, a.Status");
			sb.AppendLine("FROM tblRecipientParent ");
			sb.AppendLine("INNER JOIN tblRecipientChild c on tblRecipientParent.RecipientNum = c.RecipientNum ");
			sb.AppendLine("INNER JOIN tblStatusLog l on c.LabelNum = l.LabelNum ");
			sb.AppendLine("INNER JOIN tblStatusTypes a on l.StatusID = a.StatusID ");
			//sb.AppendLine("LEFT OUTER JOIN tblAgencyLocations locations on locations.Location = tblRecipientParent.Location ");
			sb.AppendLine("INNER JOIN (SELECT LabelNum, Max(LogID) maxId from tblStatusLog group by LabelNum) b ");
			sb.AppendLine("on l.LabelNum = b.LabelNum and l.LogID = b.maxId");
			sb.AppendLine("where tblRecipientParent.AgencyID=@AgencyId");
			sb.Append(" AND (");
			var c = String.Empty;
			for (var i = 0; i < webGroups.Count; i++)
			{
				sb.AppendLine(c + "a.WebGroup = '" + DashboardTypeStrings[webGroups[i]] + "' ");
				c = " OR ";
			}

			sb.Append(")");
			command.Parameters.AddWithValue("AgencyId", agencyId);
			command.CommandText = sb.ToString();
			command.CommandType = CommandType.Text;
			return GetRecords(command);
		}

	}
}
