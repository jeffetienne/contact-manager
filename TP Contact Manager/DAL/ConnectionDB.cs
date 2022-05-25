using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class ConnectionDb : IConnectionDb
    {
        private static readonly string ConnStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        private SqlConnection _conn;
        private SqlCommand _command;
        public SqlDataReader Reader { get; set; }
        
        public void OpenConnection()
        {
            this._conn = new SqlConnection(ConnStr);
            this._conn.Open();
        }

        public void CreateCommand()
        {
            this._command = this._conn.CreateCommand();
        }

        public void ExecuteCommand(string queryCommand)
        {
            this._command.CommandText = queryCommand;
            this.Reader = this._command.ExecuteReader();
        }

        public void CloseConnection()
        {
            this._conn.Close();
        }
    }
}