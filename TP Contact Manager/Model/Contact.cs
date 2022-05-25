namespace Model
{
    public class Contact
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
        public string Prenom { get; set; }
        public int? NumeroGroupe { get; set; }
        public string AdresseCourriel { get; set; }
        public string Tel { get; set; }
        public string Adresse { get; set; }

        public Contact()
        {

        }
        public Contact(int id, string nom, string prenom, int? groupe, string email, string telephone, string adresse)
        {
            this.id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.NumeroGroupe = groupe;
            this.AdresseCourriel = email;
            this.Tel = telephone;
            this.Adresse = adresse;
        }

        public Contact(string nom, string prenom, int? groupe, string email, string telephone, string adresse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.NumeroGroupe = groupe;
            this.AdresseCourriel = email;
            this.Tel = telephone;
            this.Adresse = adresse;
        }
    }
}
