using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class UsagerService
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public static string AjouterUsager(Usager usager, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into Usagers(identifiant, motDePasse, role)"
                                   + "values(@identifiant, @password, @role)";

                        cmd.Parameters.AddWithValue("@identifiant", usager.Identifiant);
                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", usager.Role);


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

        public static string ModifierUsager(Usager usager, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "update Usagers set role = @role, motDePasse = @password where id = @id";

                        cmd.Parameters.AddWithValue("@password", password);
                        cmd.Parameters.AddWithValue("@role", usager.Role);
                        cmd.Parameters.AddWithValue("@id", usager.Id);


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
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [identifiant]
                                          ,[role]
                                          ,[motDePasse]
                                      FROM [dbGestionnaire].[dbo].[Usagers]
                                      WHERE identifiant = @identifiant and motDePasse = @password";

                    cmd.Parameters.AddWithValue("@identifiant", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usager = new Usager();
                            usager.Identifiant = reader.GetString(0);
                            usager.Role = reader.GetInt32(1);
                        }
                    }
                    return usager;
                }
            }
        }

        public static List<UsagerViewModel> GetUsagers(string identifiant)
        {
            List<UsagerViewModel> usagers = new List<UsagerViewModel>();
            UsagerViewModel usager = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT u.id, [identifiant]
                                          ,r.nom
                                      FROM [dbGestionnaire].[dbo].[Usagers] u
                                      INNER JOIN role r
                                      ON u.role = r.id
                                      WHERE identifiant like '%" + identifiant + "%'";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usager = new UsagerViewModel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2));
                            usagers.Add(usager);
                        }
                    }
                    return usagers;
                }
            }
        }

        public static Usager GetUsager(int id)
        {
            Usager usager = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                          ,[identifiant]
                                          ,[role]
                                          ,[motDePasse]
                                      FROM [dbGestionnaire].[dbo].[Usagers]
                                      WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usager = new Usager(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2));
                        }
                    }
                    return usager;
                }
            }
        }

        public static Usager GetUsager(string identifiant)
        {
            Usager usager = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                          ,[identifiant]
                                          ,[role]
                                          ,[motDePasse]
                                      FROM [dbGestionnaire].[dbo].[Usagers]
                                      WHERE identifiant = @identifiant";

                    cmd.Parameters.AddWithValue("@identifiant", identifiant);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            usager = new Usager();
                            usager.Identifiant = reader.GetString(1);
                            usager.Role = reader.GetInt32(2);
                        }
                    }
                    return usager;
                }
            }
        }

        public static string SupprimerUsager(int id)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"delete
                                      FROM [dbGestionnaire].[dbo].[Usagers]
                                      WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    cmd.ExecuteNonQuery();
                    return "Usager supprimé avec succès";
                }
            }
        }
    }
}
