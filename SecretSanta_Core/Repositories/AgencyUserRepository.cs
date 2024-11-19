using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Data;
using SecretSanta_Core.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SecretSanta_Core.Repositories
{
    public class AgencyUserRepository : GenericRepository<AgencyContactModel>
    {
        public AgencyUserRepository(IOptions<ConnectionString> connectionString) : base(connectionString)
        {

        }

        protected override AgencyContactModel PopulateRecord(SqlDataReader reader)
        {
            var model = new AgencyContactModel();
            model.Email = reader["Email"].ToString();
            model.IsActive = reader["IsActive"].Equals(true);
            model.Archive = reader["Archive"].Equals(true);
            model.Id = reader["Id"].ToString();
            model.AgencyId = reader["AgencyId"] != DBNull.Value ? Convert.ToInt32(reader["AgencyId"]) : (int?)null;
            model.AgencyName = reader["AgencyName"].ToString();
            model.FirstName = reader["FirstName"] != DBNull.Value ? reader["FirstName"].ToString() : String.Empty;
            model.LastName = reader["LastName"] != DBNull.Value ? reader["LastName"].ToString() : String.Empty;
            model.Phone = reader["Phone"] != DBNull.Value ? reader["Phone"].ToString() : String.Empty; ;
            model.AltPhone = reader["AltPhone"] != DBNull.Value ? reader["AltPhone"].ToString() : String.Empty; ;
            model.Fax = reader["Fax"] != DBNull.Value ? reader["Fax"].ToString() : String.Empty; ;
            model.EstimateWishes = reader["EstimateWishes"] != DBNull.Value ? Convert.ToInt32(reader["EstimateWishes"]) : (int?)null;
            return model;
        }

        private UserRoleModel PopulateUserRoles(SqlDataReader reader)
        {
            var model = new UserRoleModel();
            model.UserId = reader["UserId"].ToString();
            model.RoleId = reader["RoleId"].ToString();
            model.RoleName = reader["Name"].ToString();
            return model;

        }

        public List<UserRoleModel> UserRoles()
        {
            var command = new SqlCommand
            {
                CommandText = "SELECT AspNetUserRoles.*, Name FROM AspNetUserRoles INNER JOIN AspNetRoles " +
                              "ON AspNetUserRoles.RoleId = AspNetRoles.Id",
                CommandType = CommandType.Text
            };

            return GetUserRoles(command);

        }

        public List<AgencyContactModel> GetAllAgencyContacts()
        {
            var command = new SqlCommand
            {
                CommandText =
					"SELECT AgencyName, A.Email, A.Archive, A.IsActive, A.Id, B.AgencyId, C.FirstName, C.LastName, C.Phone, C.AltPhone, C.Fax, C.EstimateWishes " +
					"FROM AspNetUsers A INNER JOIN tblAgencies B ON B.AgencyId = A.AgencyId LEFT OUTER JOIN tblAgencyContacts C ON A.Id = C.Id " +
					"WHERE Archive = 'False' AND IsConfirmed = 'True'",
                CommandType = CommandType.Text,
            };

            return GetRecords(command);
        }

		public List<AgencyContactModel> GetActiveAgencyContacts()
		{
			var command = new SqlCommand
			{
				CommandText =
					"SELECT AspNetUsers.Email, AspNetUsers.Id, tblAgencies.AgencyId, AgencyName, tblAgencyContacts.* " +
					"FROM AspNetUsers " +
					"INNER JOIN tblAgencies ON tblAgencies.AgencyId = AspNetUsers.AgencyId " +
					"LEFT OUTER JOIN tblAgencyContacts ON AspnetUsers.Id = tblAgencyContacts.Id " +
					"WHERE tblAgencies.IsActive = 'True' AND AspNetUsers.IsConfirmed = 'True' AND AspNetUsers.AgencyId IS NOT NULL AND  Archive = 'False'",
				CommandType = CommandType.Text,
			};

			return GetRecords(command);

		}

		public List<AgencyContactModel> GetContactsForAgency(int agencyId)
        {
            var command = new SqlCommand
            {
                CommandText =
					"SELECT AgencyName, A.Email, A.Archive, A.IsActive, A.Id, B.AgencyId, C.FirstName, C.LastName, C.Phone, C.AltPhone, C.Fax, C.EstimateWishes " +
					"FROM AspNetUsers A INNER JOIN tblAgencies B ON B.AgencyId = A.AgencyId LEFT OUTER JOIN tblAgencyContacts C ON A.Id = C.Id " +
					"WHERE A.AgencyId=@AgencyId AND A.IsConfirmed = 'True' AND A.Archive = 'False' ",
				CommandType = CommandType.Text,
            };

            command.Parameters.AddWithValue("AgencyId", agencyId);

            return GetRecords(command);

        }

        public List<UserRoleModel> GetUserRoles(SqlCommand command)
        {
            var list = new List<UserRoleModel>();
            var connection = new SqlConnection();
            connection.ConnectionString = _connectionString;
            command.Connection = connection;
            connection.Open();
            using (connection)
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(PopulateUserRoles(reader));
                    }
                }
            }
            return list;
        }

        public int checkLeaders(int agencyId)
        {
			var command = new SqlCommand
			{
				CommandText =
					"SELECT COUNT(*) " +
					"FROM AspNetUsers A LEFT JOIN AspNetUserRoles B ON A.Id = B.UserId LEFT JOIN AspNetRoles C ON B.RoleId = C.Id " +
					"WHERE A.AgencyId=@AgencyId AND A.IsConfirmed = 'True' AND A.Archive = 'False' AND C.Name = 'Leader'",
				CommandType = CommandType.Text,
			};

			command.Parameters.AddWithValue("AgencyId", agencyId);
			return ExecuteScalar(command);
		}
        
        public string getLeader(int agencyId)
        {
			var command = new SqlCommand
			{
				CommandText =
					"SELECT A.Id " +
					"FROM AspNetUsers A LEFT JOIN AspNetUserRoles B ON A.Id = B.UserId LEFT JOIN AspNetRoles C ON B.RoleId = C.Id " +
					"WHERE A.AgencyId=@AgencyId AND A.IsConfirmed = 'True' AND A.Archive = 'False' AND C.Name = 'Leader'",
				CommandType = CommandType.Text,
			};

			command.Parameters.AddWithValue("AgencyId", agencyId);
			return ExecuteScalarForString(command);
		}
	}
}
