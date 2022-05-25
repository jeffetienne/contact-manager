using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Role
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public string Nom { get; set; }

        public Role(int id, string nom)
        {
            this.id = id;
            this.Nom = nom;
        }
    }
}
