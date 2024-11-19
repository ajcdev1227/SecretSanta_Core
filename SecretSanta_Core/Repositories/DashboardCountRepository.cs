using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Models;
using System.Data;
using System.Text;

namespace SecretSanta_Core.Repositories
{
    public class DashboardCountRepository : GenericRepository<DashboardCountModel>
    {
        public DashboardCountRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }

        protected override DashboardCountModel PopulateRecord(SqlDataReader reader)
        {
            var counts = new DashboardCountModel();
            counts.Count = Convert.ToInt32(reader["Cnt"]);
            counts.WebGroup = reader["WebGroup"].ToString();
            return counts;
        }


        public List<DashboardCountModel> CountStatus(int agencyId)
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("SELECT Count(c.LabelNum) Cnt, a.WebGroup ");
			sb.AppendLine("FROM tblRecipientParent INNER JOIN tblRecipientChild c ON ");
            sb.AppendLine("tblRecipientParent.RecipientNum = c.RecipientNum ");
            sb.AppendLine("INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum ");
            sb.AppendLine("INNER JOIN tblStatusTypes a on a.StatusID = l.StatusID");
            sb.AppendLine("Inner JOIN (SELECT LabelNum, Max(LogID) maxId FROM tblStatusLog GROUP BY LabelNum) b ");
            sb.AppendLine("ON l.LabelNum = b.LabelNum and l.LogID = b.maxId ");
            sb.AppendLine("WHERE tblRecipientParent.AgencyID=@AgencyId ");
            sb.AppendLine("GROUP BY a.WebGroup");
            command.Parameters.AddWithValue("AgencyId", agencyId);
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecords(command);
        }


        public List<DashboardCountModel> CountRecipientStatus(int agencyId)
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("Select Count(r.RecipientNum) Cnt, a.RecipientWebGroup WebGroup ");
			sb.AppendLine("FROM tblRecipientParent r INNER JOIN tblRecipientChild c ON ");
            sb.AppendLine("r.RecipientNum = c.RecipientNum ");
            sb.AppendLine("INNER JOIN tblStatusLog l ON c.LabelNum = l.LabelNum ");
            sb.AppendLine("INNER JOIN tblStatusTypes a ON a.StatusID = l.StatusID");
            sb.AppendLine("Inner JOIN (Select LabelNum, Max(LogID) maxId FROM tblStatusLog GROUP BY LabelNum) b ");
            sb.AppendLine("ON l.LabelNum = b.LabelNum and l.LogID = b.maxId ");
            sb.AppendLine("WHERE r.AgencyID=@AgencyId AND c.[Primary] = 1 ");
            sb.AppendLine("GROUP BY RecipientWebGroup");
            command.Parameters.AddWithValue("AgencyId", agencyId);
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecords(command);
        }
    }
}
