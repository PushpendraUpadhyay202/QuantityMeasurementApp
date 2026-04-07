using System.Data.SqlClient;
using RepoLayer.Interfaces;

namespace RepoLayer.Repositories
{
    public class QuantityRepository : IQuantityRepository
    {
        private readonly string connectionString;

        public QuantityRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void SaveMeasurement(
            string category,
            string operation,
            double value1,
            string unit1,
            double? value2,
            string unit2,
            double resultValue,
            string resultUnit)
        {
            using SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string query = @"
            INSERT INTO QuantityMeasurements
            (Category, Operation, Value1, Unit1, Value2, Unit2, ResultValue, ResultUnit)
            VALUES
            (@Category, @Operation, @Value1, @Unit1, @Value2, @Unit2, @ResultValue, @ResultUnit)";

            using SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@Category", category);
            command.Parameters.AddWithValue("@Operation", operation);
            command.Parameters.AddWithValue("@Value1", value1);
            command.Parameters.AddWithValue("@Unit1", unit1);
            command.Parameters.AddWithValue("@Value2", (object?)value2 ?? DBNull.Value);
            command.Parameters.AddWithValue("@Unit2", (object?)unit2 ?? DBNull.Value);
            command.Parameters.AddWithValue("@ResultValue", resultValue);
            command.Parameters.AddWithValue("@ResultUnit", resultUnit);

            command.ExecuteNonQuery();
        }
    }
}