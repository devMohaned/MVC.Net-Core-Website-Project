using Final_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Final_Project.Services.ProductsServiceFolder
{
    public class AdtService : IAdService
    {
        private readonly ApplicationDbContext _db;

        public AdtService(ApplicationDbContext db)
        {
            _db = db;
        }

        public void AdAddedToFavourit(int Adid)
        {
            Ad entity = Find(Adid);
            if (entity.AddedToFav == null)
            {
                entity.AddedToFav = 1;
            }
            else
            {
                entity.AddedToFav += 1;
            }
            Update(entity);
        }

        public void AdPhoneRequested(int Adid)
        {
            Ad entity = Find(Adid);
            if (entity.PhoneRequested==null)
            {
                entity.PhoneRequested = 1;
            }
            else
            {
                entity.PhoneRequested += 1;
            }
            Update(entity);
        }

        public void AdViewd(int Adid)
        {
            Ad entity = Find(Adid);
            if (entity.ViewCount == null)
            {
                entity.ViewCount = 1;
            }
            else
            {
                entity.ViewCount += 1;
            }
            Update(entity);
        }

        public Ad Create(Ad Entity)
        {
            _db.Ads.Add(Entity);
            _db.SaveChanges();
            return Entity;
        }

        public bool Delete(int id)
        {
            Ad product = Find(id);
            if (product != null)
            {
                _db.Remove(product);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Ad Find(int id)
        {
            return _db.Ads.Find(id);
        }

        public List<Ad> GetAll()
        {
            return _db.Ads.Include(x=>x._Category).ToList();
        }

        public List<Ad> GetRandomAds(int adSize, int multiple)
        {

            Random rng = new Random();
            List<Ad> randomAds = _db.Ads.Include(x=>x._Category).Where<Ad>(x => x.ProductReviewed == true).Take(adSize * multiple).ToList<Ad>(); // Get #(AdSize *4) Elements
            List<Ad> shuffledAds = randomAds.OrderBy(a => rng.Next()).ToList(); // Shuffle the elements
            return shuffledAds.Take(adSize).ToList<Ad>(); // Take #Adsize Elements
        }

        public List<Ad> GetUserAd(string UserId)
        {
            return _db.Ads.Where(x => x.SellerId == UserId).ToList();
        }

        public List<Ad> GetUserAdWithReview(string UserId, bool isProductReviewed)
        {
            return _db.Ads.Where(x => x.SellerId == UserId && x.ProductReviewed == isProductReviewed).ToList();
        }
       
        public List<Ad> searchforstr(string str)
        { List<Ad> ads = new List<Ad>();
            List<Ad> adList = _db.Ads.Where(a=> a.ProductReviewed == true).ToList();
            foreach (var item in adList )
            { if ((item.AdBrief.Contains   (str, StringComparison.OrdinalIgnoreCase))|| (item.AdDescription.Contains(str, StringComparison.OrdinalIgnoreCase))|| (item.AdTitle.Contains(str, StringComparison.OrdinalIgnoreCase)))
                {
                   ads.Add(item);
                }
               

            }

            return ads;

               // return _db.Ads.Where(item=> item.AdTitle==str).ToList();
            
           
        }

        public Ad Update(Ad Entity)
        {
            _db.Ads.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }

        Ad IAdService.GetAd(int id)
        {
            return _db.Ads.Include(item => item._Category).Include(item => item._Seller).FirstOrDefault(item => item.AdId == id);

        }

        List<AdImage> IAdService.GetAdImageList(int AdId)
        {
            return _db.AdImages.Where(item => item.AdId == AdId).ToList();
        }

        List<Ad> IAdService.getYouAlsoMayLike(int? catId, int id)
        {
            List<Ad> adList = _db.Ads.Include(item => item._Category).Where(item => item.CategoryId == catId && item.ProductReviewed == true).Take(6).ToList();
            Ad ad = _db.Ads.Include(item => item._Category).FirstOrDefault(item => item.AdId == id);
            adList.Remove(ad);
            return adList;
        }
    }
}
