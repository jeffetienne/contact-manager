using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace DAL
{
    public class ContactService
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public static string AjouterContact(Contact contact)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into Contacts(nom, prenom, numeroGroupe, AdresseCourriel, Tel, adresse)"
                                   + "values(@nom, @prenom, @groupe, @email, @telephone,  @adresse)";

                        cmd.Parameters.AddWithValue("@nom", contact.Nom);
                        cmd.Parameters.AddWithValue("@prenom", contact.Prenom);
                        if (contact.NumeroGroupe != null)
                        {
                            cmd.Parameters.AddWithValue("@groupe", contact.NumeroGroupe);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@groupe", DBNull.Value);
                        }

                        cmd.Parameters.AddWithValue("@email", contact.AdresseCourriel);
                        cmd.Parameters.AddWithValue("@telephone", contact.Tel);
                        cmd.Parameters.AddWithValue("@adresse", contact.Adresse);


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

        public static string ModifierContact(Contact contact)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"UPDATE contacts
                                            SET nom = @nom, prenom = @prenom, numeroGroupe = @groupe, AdresseCourriel = @email, Tel = @telephone, adresse = @adresse
                                            WHERE id = @id";

                        cmd.Parameters.AddWithValue("@id", contact.Id);
                        cmd.Parameters.AddWithValue("@nom", contact.Nom);
                        cmd.Parameters.AddWithValue("@prenom", contact.Prenom);
                        if (contact.NumeroGroupe != null)
                        {
                            cmd.Parameters.AddWithValue("@groupe", contact.NumeroGroupe);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@groupe", DBNull.Value);
                        }
                        cmd.Parameters.AddWithValue("@email", contact.AdresseCourriel);
                        cmd.Parameters.AddWithValue("@telephone", contact.Tel);
                        cmd.Parameters.AddWithValue("@adresse", contact.Adresse);


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

        public static string SupprimerContact(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = @"DELETE FROM contacts
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
            return "Suppression reussie!!!";
        }

        public static Contact RechercherContact(int id)
        {
            Contact contact = null;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                          ,[nom]
                                          ,[prenom]
                                          ,[numeroGroupe]
                                          ,CASE WHEN [AdresseCourriel] IS NULL THEN '' ELSE [AdresseCourriel] END
                                          ,[Tel]
                                          ,CASE WHEN adresse IS NULL THEN '' ELSE adresse END
                                      FROM [dbGestionnaire].[dbo].[contacts]
                                      WHERE id = @id";

                    cmd.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int? groupe = null;
                            if (!reader.IsDBNull(3))
                            {
                                groupe = reader.GetInt32(3);
                            }
                            contact = new Contact(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), groupe, reader.GetString(4), reader.GetString(5), reader.GetString(6));
                        }
                    }
                }
            }
            return contact;
        }

        public static bool ContactExiste(string nom, string prenom)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(Id)
                                      FROM [dbGestionnaire].[dbo].[contacts]
                                      WHERE nom = @nom and prenom = @prenom";

                    cmd.Parameters.AddWithValue("@nom", nom);
                    cmd.Parameters.AddWithValue("@prenom", prenom);

                    object nombre = cmd.ExecuteScalar();

                    return (int)nombre > 0;
                }
            }
        }

        public static bool ContactExiste(string telephone)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(Id)
                                      FROM [dbGestionnaire].[dbo].[contacts]
                                      WHERE Tel = @telephone";

                    cmd.Parameters.AddWithValue("@telephone", telephone);

                    object nombre = cmd.ExecuteScalar();

                    return (int)nombre > 0;
                }
            }
        }

        public static bool ContactExists(string email)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT count(Id)
                                      FROM [dbGestionnaire].[dbo].[contacts]
                                      WHERE AdresseCourriel = @email";

                    cmd.Parameters.AddWithValue("@email", email);

                    object nombre = cmd.ExecuteScalar();

                    return (int)nombre > 0;
                }
            }
        }

        public static List<ContactViewModel> AfficherContact()
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT c.[id]
                                          ,c.[nom]
                                          ,[prenom]
                                          ,CASE WHEN g.id IS NULL THEN '' ELSE g.nom END
                                          ,CASE WHEN [AdresseCourriel] IS NULL THEN '' ELSE [AdresseCourriel] END
                                          ,[Tel]
                                          ,CASE WHEN adresse IS NULL THEN '' ELSE adresse END
                                      FROM [dbGestionnaire].[dbo].[contacts] c
                                      LEFT JOIN dbo.groupe g
                                      ON c.numeroGroupe = g.id";

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

        public static List<ContactViewModel> RechercherContacts(string critere, string valeur)
        {
            List<ContactViewModel> contacts = new List<ContactViewModel>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    if (critere == "Email")
                    {
                        critere = "AdresseCourriel";
                    }
                    if (critere == "Telephone")
                    {
                        critere = "Tel";
                    }
                    if (critere == "Groupe")
                    {
                        cmd.CommandText = @"SELECT c.[id]
                                          ,c.[nom]
                                          ,[prenom]
                                          ,CASE WHEN g.id IS NULL THEN '' ELSE g.nom END
                                          ,CASE WHEN [AdresseCourriel] IS NULL THEN '' ELSE [AdresseCourriel] END
                                          ,[Tel]
                                          ,CASE WHEN adresse IS NULL THEN '' ELSE adresse END
                                      FROM [dbGestionnaire].[dbo].[contacts] c
                                      LEFT JOIN dbo.groupe g
                                      ON c.numeroGroupe = g.id
                                      WHERE g.nom LIKE '%" + valeur + "%'";
                    }
                    else
                    {
                        cmd.CommandText = @"SELECT c.[id]
                                          ,c.[nom]
                                          ,[prenom]
                                          ,CASE WHEN g.id IS NULL THEN '' ELSE g.nom END
                                          ,CASE WHEN [AdresseCourriel] IS NULL THEN '' ELSE [AdresseCourriel] END
                                          ,[Tel]
                                          ,CASE WHEN adresse IS NULL THEN '' ELSE adresse END
                                      FROM [dbGestionnaire].[dbo].[contacts] c
                                      LEFT JOIN dbo.groupe g
                                      ON c.numeroGroupe = g.id
                                      WHERE c." + critere + " LIKE '%" + valeur + "%'";
                    }
                    

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
    }
}
