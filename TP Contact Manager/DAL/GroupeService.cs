using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class GroupeService
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public static List<Groupe> GetGroupes()
        {
            List<Groupe> groupes = new List<Groupe>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, nom from groupe";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Groupe groupe = new Groupe(reader.GetInt32(0), reader.GetString(1));
                            groupes.Add(groupe);
                        }
                    }
                    return groupes;
                }
            }
        }

        public static Groupe GetGroupe(int id)
        {
            Groupe groupe = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, nom from groupe where id = @id";

                    cmd.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            groupe = new Groupe(reader.GetInt32(0), reader.GetString(1));
                        }
                    }
                    return groupe;
                }
            }
        }
    }
}
