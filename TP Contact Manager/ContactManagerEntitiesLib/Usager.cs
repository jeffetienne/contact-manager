using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerEntitiesLib
{
    public class Usager
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Identifiant { get; set; }
        public Usager()
        {

        }
        public Usager(string email, string identifiant)
        {
            this.Email = email;
            this.Identifiant = identifiant;
        }
    }
}
