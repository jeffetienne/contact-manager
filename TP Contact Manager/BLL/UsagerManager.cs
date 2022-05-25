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
    public class UsagerManager
    {
        public static string AjouterUsager(Usager usager, string password)
        {
            return UsagerService.AjouterUsager(usager, password);
        }

        public static string ModifierUsager(Usager usager, string password)
        {
            return UsagerService.ModifierUsager(usager, password);
        }

        public static Usager Login(string username, string password)
        {
            return UsagerService.Login(username, password);
        }

        public static List<UsagerViewModel> GetUsagers(string identifiant)
        {
            return UsagerService.GetUsagers(identifiant);
        }

        public static Usager GetUsager(int id)
        {
            return UsagerService.GetUsager(id);
        }

        public static Usager GetUsager(string identifiant)
        {
            return UsagerService.GetUsager(identifiant);
        }

        public static string SupprimerUsager(int id)
        {
            return UsagerService.SupprimerUsager(id);
        }
    }
}
