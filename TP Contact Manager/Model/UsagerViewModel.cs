using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class UsagerViewModel
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public string Identifiant { get; set; }
        public string Role { get; set; }
        public UsagerViewModel(int id, string identifiant, string role)
        {
            this.id = id;
            this.Identifiant = identifiant;
            this.Role = role;
        }
    }
}
