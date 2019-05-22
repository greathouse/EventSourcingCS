using System;
using System.Data.SQLite;
using System.IO;
using GreenMoonSoftware.EventSourcing.Database;
using GreenMoonSoftware.EventSourcing.SqlLite;

namespace GreenMoonSoftware.EventSourcing.SqlLiteTest
{
    public class TestHelper
    {
        public static DatabaseConfiguration Configuration()
        {
            return new SqlLiteDatabaseConfiguration
            {
                ConnectionString = $"Data Source={Path.GetTempFileName()};Version=3;",
                TableName = "TestEvent"
            };
        }
        
        public static void CreateEventTable(DatabaseConfiguration configuration)
        {
            using (var conn = configuration.CreateConnection())
            {
                conn.Open();
                var createTable = conn.CreateCommand();
                var tableCommandText = $@"create table {configuration.TableName} (
                                            id TEXT,
                                            aggregateId TEXT,
                                            eventType TEXT,
                                            eventDateTime TEXT,
                                            savedTimestamp TEXT,
                                            data BLOB)";
                createTable.CommandText = tableCommandText;
                createTable.ExecuteNonQuery();
            }
        }
    }
}