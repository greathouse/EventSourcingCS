using System.Data;
using System.Data.SQLite;
using GreenMoonSoftware.EventSourcing.Database;

namespace GreenMoonSoftware.EventSourcing.SqlLite
{
    public class SqlLiteDatabaseConfiguration : DatabaseConfiguration
    {
        public override IDbConnection CreateConnection()
        {
            return new SQLiteConnection(ConnectionString);
        }
    }
}