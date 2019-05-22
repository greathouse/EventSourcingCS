using System.Data;

namespace GreenMoonSoftware.EventSourcing.Database
{
    public abstract class DatabaseConfiguration
    {
        public string ConnectionString { set; get; }
        public string TableName { get; set; }

        public abstract IDbConnection CreateConnection();
    }
}