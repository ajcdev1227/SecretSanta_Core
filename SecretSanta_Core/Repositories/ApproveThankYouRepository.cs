using Microsoft.Data.SqlClient;
using System.Data;
using SecretSanta_Core.Models;
using Microsoft.Extensions.Options;

namespace SecretSanta_Core.Repositories
{
    public class ApproveThankYouRepository: GenericRepository<ApproveThankYouModel>
    {
        public ApproveThankYouRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }

        protected override ApproveThankYouModel PopulateRecord(SqlDataReader reader)
        {
            var model = new ApproveThankYouModel();
            model.DonorThankYouId = Convert.ToInt32(reader["DonorThankYouId"]);
            model.RecipientName = reader["Name"].ToString();
            model.ThankYouDate = Convert.ToDateTime(reader["ThankYouDate"]).ToShortDateString();
            model.Image = reader["Image"] != DBNull.Value ? reader["Image"].ToString() : String.Empty;
            model.Message = reader["Message"] != DBNull.Value ? reader["Message"].ToString() : String.Empty;
            model.Approved = Convert.ToBoolean(reader["Approved"]);
            return model;
        }

        public List<ApproveThankYouModel> GetThankYous()
        {
            var command = new SqlCommand
            {
                CommandText =
                    "select p.Name, t.DonorThankYouId, t.ThankYouDate, t.[Image], t.[Message], t.Approved  " +
                    "from tblRecipientParent p inner join tblDonorThankYou t  " +
                    "on t.RecipientNum = p.RecipientNum",

                CommandType = CommandType.Text
            };
            return GetRecords(command);
        }
    }
}
