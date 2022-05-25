using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace BLL
{
    public class RoleManager
    {
        public static List<Role> GetRoles()
        {
            RoleService.Connection = new ConnectionDb();
            return RoleService.GetRoles();
        }
    }
}
