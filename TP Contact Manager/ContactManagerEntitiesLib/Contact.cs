using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerEntitiesLib
{
    public class Contact
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public Contact()
        {

        }
        public Contact(string nom, string prenom, string email, string telephone, string adresse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Email = email;
            this.Telephone = telephone;
            this.Adresse = adresse;
        }
    }
}
