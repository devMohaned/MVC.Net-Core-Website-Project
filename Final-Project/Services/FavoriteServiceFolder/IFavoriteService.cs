using Final_Project.Models;

namespace Final_Project.Services.FavoriteServiceFolder
{
    public interface IFavoriteService : IGeneralDataService<Favorite>,
        ICompositeDataService<Favorite, string, int>
    {
        List<Favorite> GetUserFavorites(string id);
        bool CheckIfFavoritaddtouser(string customerid, int favouritid);

        bool removeProductFavorites(int productId);
    }
}
