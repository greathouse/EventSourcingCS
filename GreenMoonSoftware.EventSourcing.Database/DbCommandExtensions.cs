using System.Data;

namespace GreenMoonSoftware.EventSourcing.Database
{
    public static class DbCommandExtensions
    {
        public static void AddWithValue (this IDbCommand command, string name, object value)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            command.Parameters.Add(parameter);
        }
    }
}