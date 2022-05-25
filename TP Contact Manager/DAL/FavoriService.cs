using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class FavoriService
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public static string AjouterFavori(Favori favori)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into Favori(contact)"
                                   + "values(@contact)";

                        cmd.Parameters.AddWithValue("@contact", favori.Contact);



                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "Contact ajouté aux favoris!!!";
        }

        public static Favori GetFavoris(int id)
        {
            Favori favori = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT id, contact
                                          FROM [dbGestionnaire].[dbo].[Favori]
                                        WHERE contact = @contact";

                    cmd.Parameters.AddWithValue("@contact", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            favori = new Favori(reader.GetInt32(0), reader.GetInt32(1));
                        }
                    }
                    return favori;
                }
            }
        }

        public static List<ContactViewModel> GetFavoris()
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT f.[id]
                                              ,c.[nom]
	                                          ,[prenom]
	                                          ,CASE WHEN g.id IS NULL THEN '' ELSE g.nom END
	                                          ,CASE WHEN [AdresseCourriel] IS NULL THEN '' ELSE [AdresseCourriel] END
	                                          ,[Tel]
	                                          ,CASE WHEN adresse IS NULL THEN '' ELSE adresse END
                                          FROM [dbGestionnaire].[dbo].[Favori] f
                                          INNER JOIN contacts c
                                          ON f.contact = c.id
                                          LEFT JOIN groupe g
                                          ON g.id = c.numeroGroupe";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            ContactViewModel contact = new ContactViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetString(4), reader.GetString(5), reader.GetString(6));
                            contacts.Add(contact);
                        }
                    }
                    return contacts;
                }
            }
        }

        public static string Supprimerfavori(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM favori
                                            WHERE id = @id";

                        cmd.Parameters.AddWithValue("@id", id);


                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "Contact retiré des favoris!!!";
        }
    }
}
