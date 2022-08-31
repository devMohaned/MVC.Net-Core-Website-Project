using Final_Project.Models;

namespace Final_Project.Services.ProductImageServiceFolder
{
    public class AdImageService : IAdImageService
    {
        private readonly ApplicationDbContext _db;

        public AdImageService(ApplicationDbContext db)
        {
            _db = db;
        }

        public AdImage Create(AdImage Entity)
        {
            _db.AdImages.Add(Entity);
            _db.SaveChanges();
            return Entity;
        }

        public bool Delete(int id)
        {
            AdImage productImage = Find(id);
            if (productImage != null)
            {
                _db.AdImages.Remove(productImage);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public AdImage Find(int id)
        {
            return _db.AdImages.Find(id);
        }

        public List<AdImage> GetAll()
        {
            return _db.AdImages.ToList();
        }

        public AdImage Update(AdImage Entity)
        {
            _db.AdImages.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }
    }
}
