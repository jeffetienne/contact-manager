using Model;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ContactManager
    {
        public static string AjouterContact(Contact contact)
        {
            return ContactService.AjouterContact(contact);
        }

        public static string ModifierContact(Contact contact)
        {
            return ContactService.ModifierContact(contact);
        }

        public static string SupprimerContact(int id)
        {
            return ContactService.SupprimerContact(id);
        }

        public static Contact RechercherContact(int id)
        {
            return ContactService.RechercherContact(id);
        }

        public static bool ContactExiste(string nom, string prenom)
        {
            return ContactService.ContactExiste(nom, prenom);
        }

        public static bool ContactExiste(string telephone)
        {
            return ContactService.ContactExiste(telephone);
        }

        public static bool ContactExists(string email)
        {
            return ContactService.ContactExists(email);
        }

        public static List<ContactViewModel> AfficherContact()
        {
            return ContactService.AfficherContact();
        }

        public static List<ContactViewModel> RechercherContacts(string critere, string valeur)
        {
            return ContactService.RechercherContacts(critere, valeur);
        }
    }
}
