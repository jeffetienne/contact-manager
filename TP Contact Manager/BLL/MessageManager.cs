using Model;
using DAL;
using System.Collections.Generic;

namespace BLL
{
    public class MessageManager
    {
        public static string EnvoyerMessage(Message message)
        {
            return MessageService.EnvoyerMessage(message);
        }

        public static List<MessageViewModel> VoirMessages()
        {
            return MessageService.VoirMessages();
        }

        public static List<Message> VoirMessages(int destinataire)
        {
            return MessageService.VoirMessages(destinataire);
        }
    }
}
