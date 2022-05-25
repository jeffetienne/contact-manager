using ContactManagerEntitiesLib;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerBLLLib
{
    public class ContactManager
    {
        public static string AjouterContact(Contact contact)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString: @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30"))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into Contacts(nom, prenom, numeroGroupe, AdresseCourriel, Tel, adresse)"
                                   + "values(@nom, @prenom, @groupe, @email, @telephone,  @adresse)";

                        cmd.Parameters.AddWithValue("@nom", contact.Nom);
                        cmd.Parameters.AddWithValue("@prenom", contact.Prenom);
                        cmd.Parameters.AddWithValue("@groupe", 1);
                        cmd.Parameters.AddWithValue("@email", contact.Email);
                        cmd.Parameters.AddWithValue("@telephone", contact.Telephone);
                        cmd.Parameters.AddWithValue("@adresse", contact.Adresse);


                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Done");
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
                using (SqlConnection conn = new SqlConnection(connectionString: @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30"))
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
                        cmd.Parameters.AddWithValue("@groupe", 1);
                        cmd.Parameters.AddWithValue("@email", contact.Email);
                        cmd.Parameters.AddWithValue("@telephone", contact.Telephone);
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
                using (SqlConnection conn = new SqlConnection(connectionString: @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30"))
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

        public static List<Contact> AfficherContact()
        {
            List<Contact> contacts = new List<Contact>();
            string connStr = @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                          ,[nom]
                                          ,[prenom]
                                          ,[numeroGroupe]
                                          ,[AdresseCourriel]
                                          ,[Tel]
                                          ,[adresse]
                                      FROM [dbGestionnaire].[dbo].[contacts]";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact contact = new Contact();
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            //contact. = reader.GetDateTime(3);
                            contact.Email = reader.GetString(4);
                            contact.Telephone = reader.GetString(5);
                            contact.Adresse = reader.GetString(6);
                            contacts.Add(contact);
                        }
                    }
                    return contacts;
                }
            }
        }

        public static List<Contact> RechercherContacts(string critere, string valeur)
        {
            List<Contact> contacts = new List<Contact>();
            string connStr = @"Data Source=JEFFETIENNEPC;Initial Catalog=dbGestionnaire;User ID=sa;Password=d0m5av5pr1n9fd5;Connect Timeout=30";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                          ,[nom]
                                          ,[prenom]
                                          ,[numeroGroupe]
                                          ,[AdresseCourriel]
                                          ,[Tel]
                                          ,[adresse]
                                      FROM [dbGestionnaire].[dbo].[contacts]
                                      WHERE " + critere + " = @valeur";

                    cmd.Parameters.AddWithValue("@valeur", valeur);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Contact contact = new Contact();
                            contact.Id = reader.GetInt32(0);
                            contact.Nom = reader.GetString(1);
                            contact.Prenom = reader.GetString(2);
                            //contact. = reader.GetDateTime(3);
                            contact.Email = reader.GetString(4);
                            contact.Telephone = reader.GetString(5);
                            contact.Adresse = reader.GetString(6);
                            contacts.Add(contact);
                        }
                    }
                    return contacts;
                }
            }
        }
    }
}
