using Final_Project.Models;

namespace Final_Project.Services.BrandServiceFolder
{
    public class BrandService : IBrandService
    {
        private readonly ApplicationDbContext _db;

        public BrandService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Brand Create(Brand Entity)
        {
            _db.Brands.Add(Entity);
            _db.SaveChanges();
            return Entity;
        }

        public bool Delete(int id)
        {
            Brand brand = Find(id);
            if (brand != null)
            {
                _db.Brands.Remove(brand);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Brand Find(int id)
        {
            return _db.Brands.Find(id);
        }

        public List<Brand> GetAll()
        {
            return _db.Brands.ToList();
        }

        public List<Brand> GetCategoryBrands(int id)
        {
            return _db.Brands.Where(x => x.CategoryId == id).ToList();
        }

        public Brand Update(Brand Entity)
        {
            _db.Brands.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }
    }
}
