using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Model;

namespace DAL
{
    public class MessageService
    {
        private static string connStr = ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
        public static string EnvoyerMessage(Message message)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "insert into message(destinataire, contenu, dateEnvoi)"
                                   + "values(@destinataire, @contenu, @dateEnvoi)";

                        cmd.Parameters.AddWithValue("@destinataire", message.Destinataire);
                        cmd.Parameters.AddWithValue("@contenu", message.Contenu);
                        cmd.Parameters.AddWithValue("@dateEnvoi", message.DateEnvoi);



                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {

                return e.Message;
            }
            return "Message Envoyé avec succès!!!";
        }

        public static List<MessageViewModel> VoirMessages()
        {
            MessageViewModel message = null;
            List<MessageViewModel> messages = new List<MessageViewModel>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT m.[id]
                                              ,c.Prenom + ' ' + c.Nom
                                              ,[contenu]
                                              ,[dateEnvoi]
                                          FROM [dbGestionnaire].[dbo].[message] m
                                          INNER JOIN contacts c
                                          ON m.destinataire = c.Id";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message = new MessageViewModel(reader.GetString(1), reader.GetString(2), reader.GetDateTime(3));
                            messages.Add(message);
                        }
                    }
                    return messages;
                }
            }
        }

        public static List<Message> VoirMessages(int destinataire)
        {
            Message message = null;
            List<Message> messages = new List<Message>();
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT [id]
                                              ,[destinataire]
                                              ,[contenu]
                                              ,[dateEnvoi]
                                          FROM [dbGestionnaire].[dbo].[message]
                                          WHERE destinataire = @destinataire";

                    cmd.Parameters.AddWithValue("@destinataire", destinataire);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message = new Message(reader.GetInt32(0), reader.GetInt32(1), reader.GetString(2), reader.GetDateTime(3));
                            messages.Add(message);
                        }
                    }
                    return messages;
                }
            }
        }
    }
}
