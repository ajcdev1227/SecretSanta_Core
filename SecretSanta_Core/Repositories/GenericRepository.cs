using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SecretSanta_Core.Data;

namespace SecretSanta_Core.Repositories
{
    public abstract class GenericRepository<T> where T : class
        {
            protected readonly string _connectionString;

            public GenericRepository(IOptions<ConnectionString> connectionString)
            {

                _connectionString = connectionString.Value.DefaultConnectionString;
            }


            protected abstract T PopulateRecord(SqlDataReader reader);


            public List<T> GetRecords(SqlCommand command)
            {
                var list = new List<T>();
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
                            list.Add(PopulateRecord(reader));
                        }
                    }
                }

                return list;
            }

            public T GetRecord(SqlCommand command)
            {
                T record = default(T);
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
                            record = PopulateRecord(reader);
                        }
                    }
                }

                return record;
            }

            public void ExecuteNonQuery(SqlCommand command)
            {
                var connection = new SqlConnection();
                connection.ConnectionString = _connectionString;
                command.Connection = connection;
                connection.Open();
                using (connection)
                {
                    command.ExecuteNonQuery();
                }
            }

            public SqlDataReader ExecuteQuery(SqlCommand command)
            {
                var connection = new SqlConnection();
                connection.ConnectionString = _connectionString;
                command.Connection = connection;
                connection.Open();
                SqlDataReader reader;
                using (connection)
                {
                    reader = command.ExecuteReader();
                }

                return reader;
            }

            public int ExecuteScalar(SqlCommand command)
            {
                var connection = new SqlConnection();
                connection.ConnectionString = _connectionString;
                command.Connection = connection;
                connection.Open();
                var count = 0;
                using (connection)
                {
                    count = Convert.ToInt32(command.ExecuteScalar());
                }

                return count;
            }

            public string ExecuteScalarForString(SqlCommand command)
            {
                var connection = new SqlConnection();
                connection.ConnectionString = _connectionString;
                command.Connection = connection;
                connection.Open();
                object result;
                using (connection)
                {
                    result = command.ExecuteScalar();
                }

                if (result == DBNull.Value)
                {
                    return String.Empty;
                }

                return result.ToString();
            }


    }
}
