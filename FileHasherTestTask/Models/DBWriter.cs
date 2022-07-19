using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace FileHasherTestTask.Models
{
    internal class DBWriter
    {
        private static string? _connectionString;
        static DBWriter()
        {
            ConnectionStringSettings settings = ConfigurationManager.ConnectionStrings["FileHashesDB"];
            if (settings is not null)
                _connectionString = settings.ConnectionString;
        }

        public static void Write(string hash)
        {
            using (SqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = new("AddHash", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    SqlParameter parameter = new();
                    parameter.ParameterName = "@hashStr";
                    parameter.SqlDbType = SqlDbType.NVarChar;
                    parameter.Value = hash;
                    command.Parameters.Add(parameter);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}