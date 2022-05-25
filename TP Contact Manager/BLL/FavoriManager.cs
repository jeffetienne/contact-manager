using System.Collections.Generic;
using DAL;
using Model;

namespace BLL
{
    public class FavoriManager
    {
        public static string AjouterFavori(Favori favori)
        {
            return FavoriService.AjouterFavori(favori);
        }

        public static List<ContactViewModel> GetFavoris()
        {
            return FavoriService.GetFavoris();
        }

        public static Favori GetFavori(int id)
        {
            return FavoriService.GetFavoris(id);
        }

        public static string Supprimerfavori(int id)
        {
            return FavoriService.Supprimerfavori(id);
        }
    }
}
