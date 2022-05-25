namespace Model
{
    public class Favori
    {
        private int id;
        public int Id
        {
            get
            {
                return this.id;
            }
        }
        public int Contact { get; set; }
        public Favori(int id, int contact)
        {
            this.id = id;
            this.Contact = contact;
        }
    }
}
