using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Message
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public int Destinataire { get; set; }
        public Contact Contact { get; set; }
        public string Contenu { get; set; }
        public DateTime DateEnvoi { get; set; }

        public Message(int id, int destinataire, string contenu, DateTime dateEnvoi)
        {
            this.id = id;
            this.Destinataire = destinataire;
            this.Contenu = contenu;
            this.DateEnvoi = dateEnvoi;
        }

        public Message(int destinataire, string contenu, DateTime dateEnvoi)
        {
            this.Destinataire = destinataire;
            this.Contenu = contenu;
            this.DateEnvoi = dateEnvoi;
        }
    }
}
