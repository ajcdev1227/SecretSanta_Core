using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;
using System.Text;
using SecretSanta_Core.Models;

namespace SecretSanta_Core.Repositories
{
    public class LetterSantaRepository : GenericRepository<AdoptLetterModel>
    {
        public LetterSantaRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }

        protected override AdoptLetterModel PopulateRecord(SqlDataReader reader)
        {
            var letter = new AdoptLetterModel();
            letter.LetterId = Convert.ToInt32(reader["LetterID"]);
            letter.Letter = reader["Letter"].ToString();
            letter.LetterSummary = reader["LetterSummary"].ToString();
            return letter;
        }

        public List<AdoptLetterModel> GetLetters(int statusId)
        {
            var command = new SqlCommand();
            var sb = new StringBuilder();
            sb.AppendLine("select tblLetterSanta.LetterId, tblLetterSanta.Letter, tblLetterSanta.LetterSummary ");
            sb.AppendLine("from tblLetterSanta inner join tblLetterStatus ");
            sb.AppendLine("on tblLetterSanta.LetterId = tblLetterStatus.LetterID ");
            sb.AppendLine(
                "Inner Join (Select LetterId , Max(LetterStatusId) maxId from tblLetterStatus group by LetterId) b ");
            sb.AppendLine("on tblLetterStatus.LetterId = b.LetterId and tblLetterStatus.LetterStatusId = b.maxId ");
            sb.AppendLine("and StatusID = @StatusID");
            sb.AppendLine("where tblLetterSanta.IsActive = 1");
            command.Parameters.AddWithValue("StatusID", statusId);
            command.CommandText = sb.ToString();
            command.CommandType = CommandType.Text;
            return GetRecords(command);
        }
    }
}
