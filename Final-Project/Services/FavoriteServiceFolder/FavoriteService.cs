using Final_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services.FavoriteServiceFolder
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ApplicationDbContext _db;

        public FavoriteService(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool CheckIfFavoritaddtouser(string customerid, int adid)
        {
            var item = Find(customerid, adid);
            return item == null ? false : true;
        }

        public Favorite Create(Favorite Entity)
        {
            bool added= CheckIfFavoritaddtouser(Entity.CustomerId, Entity.AdtId);
            if (!added)
            {
                _db.Favorites.Add(Entity);
                _db.SaveChanges();
            }

            return Entity;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="firstKey">CustomerId</param>
        /// <param name="secondKey">AdId</param>
        /// <returns></returns>
        public bool Delete(string firstKey, int secondKey)
        {
            Favorite favorite = Find(firstKey, secondKey);
            if (favorite != null)
            {
                _db.Favorites.Remove(favorite);
                _db.SaveChanges();
                return true;
            }
            return false;

        }


        public Favorite Find(string firstKey, int secondKey)
        {
           
            return _db.Favorites.FirstOrDefault(item => item.CustomerId == firstKey && item.AdtId == secondKey);
        }

        public List<Favorite> GetAll()
        {
            return _db.Favorites.ToList();
        }

        public List<Favorite> GetUserFavorites(string id)
        {
            return _db.Favorites.Include(x=>x._Ad).Where(x=>x.CustomerId==id && x._Ad.ProductReviewed == true).ToList();
            //return _db.Favorites.Include(item => item._Product)
            //    .Where(item => item.CustomerId == id).ToList();
        }

        public bool removeProductFavorites(int productId)
        {
            List<Favorite> favorites = _db.Favorites.Where(x => x.AdtId == productId).ToList();
            for (int i = 0; i < favorites.Count; i++)
            {
                Favorite item = favorites[i];
                _db.Favorites.Remove(item);
            }
            return true;
        }

        public Favorite Update(Favorite Entity)
        {
            _db.Favorites.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }
    }
}
