using System;
using System.Data;
using System.Data.SqlClient;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlServer
{
    public class SqlServerDatabaseConfiguration : DatabaseConfiguration
    {
        public override IDbConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}