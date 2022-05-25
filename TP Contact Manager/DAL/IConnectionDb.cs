using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public interface IConnectionDb
    {
        void OpenConnection();
        void CreateCommand();
        void ExecuteCommand(string queryCommand);
        void CloseConnection();
    }
}