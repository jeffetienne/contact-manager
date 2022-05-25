using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class RoleService
    {
        public static IConnectionDb Connection { get; set; }
        public static List<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            SqlDataReader reader = null;

            Connection.OpenConnection();
            Connection.CreateCommand();
            Connection.ExecuteCommand(@"SELECT id, nom from role");

            reader = ((ConnectionDb) Connection).Reader;
            
            while (reader.Read())
            {
                Role role = new Role(reader.GetInt32(0), reader.GetString(1));
                roles.Add(role);
            }

            Connection.CloseConnection();
            
            return roles;
        }
    }
}
