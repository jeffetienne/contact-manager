using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Usager
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
        public int Role { get; set; }
        public Usager()
        {

        }
        public Usager(int id, string identifiant, int role)
        {
            this.id = id;
            this.Identifiant = identifiant;
            this.Role = role;
        }

        public Usager(string identifiant, int role)
        {
            this.Identifiant = identifiant;
            this.Role = role;
        }
    }
}
