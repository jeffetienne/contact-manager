using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class GroupeManager
    {
        public static List<Groupe> GetGroupes()
        {
            return GroupeService.GetGroupes();
        }

        public static Groupe GetGroupe(int id)
        {
            return GroupeService.GetGroupe(id);
        }
    }
}
