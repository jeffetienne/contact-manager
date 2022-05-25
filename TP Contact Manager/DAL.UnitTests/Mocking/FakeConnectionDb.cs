
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL.UnitTests.Mocking
{
    public class FakeConnectionDb : IConnectionDb
    {
        public string Connection { get; set; }
        public string Command { get; set; }
        public string Reader { get; set; }
        public void OpenConnection()
        {
            this.Connection = "a";
        }

        public void CreateCommand()
        {
            this.Command = this.Connection;
        }

        public void ExecuteCommand(string queryCommand)
        {
            this.Reader = queryCommand + this.Command;
        }

        public void CloseConnection()
        {
            this.Reader = null;
            this.Command = null;
            this.Connection = null;
        }
    }
}