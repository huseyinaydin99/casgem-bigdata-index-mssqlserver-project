using System.Data.SqlClient;

namespace Casgem.BigData.Index.MSSQLServer.Project.DAL.Constant
{
    public static class Constans
    {
        //internal static string CONNECTION_STRING { get; } = "Server = DESKTOP-13123BI; Initial Catalog = CARPLATES; Integrated Security = true;";
        internal static SqlConnection GetConnection()
        {
            var connection = new SqlConnection("Server = DESKTOP-13123BI; Initial Catalog = CARPLATES; Integrated Security = true;");
            return connection;
        }
    }
}