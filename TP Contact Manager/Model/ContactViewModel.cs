namespace Model
{
    public class ContactViewModel
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
        public string Groupe { get; set; }
        public string AdresseCourriel { get; set; }
        public string Tel { get; set; }
        public string Adresse { get; set; }

        public ContactViewModel()
        {

        }
        public ContactViewModel(int id, string nom, string prenom, string groupe, string email, string telephone, string adresse)
        {
            this.id = id;
            this.Nom = nom;
            this.Prenom = prenom;
            this.Groupe = groupe;
            this.AdresseCourriel = email;
            this.Tel = telephone;
            this.Adresse = adresse;
        }

        public ContactViewModel(string nom, string prenom, string groupe, string email, string telephone, string adresse)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Groupe = groupe;
            this.AdresseCourriel = email;
            this.Tel = telephone;
            this.Adresse = adresse;
        }
    }
}
