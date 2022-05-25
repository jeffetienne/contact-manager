using ContactManagerEntitiesLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerBLLLib
{
    public class UsagerManager
    {
        public static string AjouterUsager(Usager usager, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString: @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30"))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into Usagers(identifiant, motDePasse, email)"
                                   + "values(@identifiant, @password, @email, @email)";

                        cmd.Parameters.AddWithValue("@nom", usager.Identifiant);
                        cmd.Parameters.AddWithValue("@prenom", usager.Email);
                        cmd.Parameters.AddWithValue("@password", password);
                        


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "Enregistrement reussi!!!";
        }
        
        public static Usager Login(string username, string password)
        {
            Usager usager = null;
            string connStr = @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                          ,[identifiant]
                                          ,[motDePasse]
                                          ,[email]
                                      FROM [dbGestionnaire].[dbo].[Usagers]
                                      WHERE identifiant = @identifiant and motDePasse = @password";

                    cmd.Parameters.AddWithValue("@identifiant", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usager = new Usager();
                            usager.Id = reader.GetInt32(0);
                            usager.Identifiant = reader.GetString(1);
                            usager.Email = reader.GetString(3);
                        }
                    }
                    return usager;
                }
            }
        }
    }
}
