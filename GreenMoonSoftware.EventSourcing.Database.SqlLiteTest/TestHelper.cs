using System;
using System.Data.SQLite;
using System.IO;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLiteTest
{
    public class TestHelper
    {
        public static DatabaseConfiguration Configuration()
        {
            return new DatabaseConfiguration
            {
                ConnectionString = $"Data Source={Path.GetTempFileName()};Version=3;",
                TableName = "TestEvent"
            };
        }
        
        public static void CreateEventTable(DatabaseConfiguration configuration)
        {
            var conn = new SQLiteConnection(configuration.ConnectionString);
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