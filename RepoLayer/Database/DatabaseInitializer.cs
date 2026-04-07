using System.Data.SqlClient;

namespace RepoLayer.Database
{
    public static class DatabaseInitializer
    {
        public static void Initialize(string connectionString)
        {
            // temporarily connect to master to create db in the sql server
            var masterConnection =
                connectionString.Replace("Database=QuantityMeasurementDB", "Database=master");

            using SqlConnection connection = new SqlConnection(masterConnection);

            connection.Open();

            string createDbQuery = @"
            IF DB_ID('QuantityMeasurementDB') IS NULL
            CREATE DATABASE QuantityMeasurementDB";

            using SqlCommand createDb = new SqlCommand(createDbQuery, connection);

            createDb.ExecuteNonQuery();

            connection.Close();

            // connect to actual database
            using SqlConnection dbConnection = new SqlConnection(connectionString);

            dbConnection.Open();

            string createTableQuery = @"
            IF OBJECT_ID('QuantityMeasurements', 'U') IS NULL
            CREATE TABLE QuantityMeasurements
            (
                Id INT IDENTITY(1,1) PRIMARY KEY,
                Category NVARCHAR(50),
                Operation NVARCHAR(50),
                Value1 FLOAT,
                Unit1 NVARCHAR(20),
                Value2 FLOAT NULL,
                Unit2 NVARCHAR(20) NULL,
                ResultValue FLOAT,
                ResultUnit NVARCHAR(20),
                CreatedAt DATETIME DEFAULT GETDATE()
            )";

            using SqlCommand createTable = new SqlCommand(createTableQuery, dbConnection);

            createTable.ExecuteNonQuery();
        }
    }
}