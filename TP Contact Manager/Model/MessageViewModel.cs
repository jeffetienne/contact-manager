using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MessageViewModel
    {
        public string Destinataire { get; set; }
        public string Contenu { get; set; }
        public DateTime DateEnvoi { get; set; }

        public MessageViewModel(string destinataire, string contenu, DateTime dateEnvoi)
        {
            this.Destinataire = destinataire;
            this.Contenu = contenu;
            this.DateEnvoi = dateEnvoi;
        }
    }
}
