using Final_Project.Models;

namespace Final_Project.Services.CategoryServiceFolder
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _db;

        public CategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Category Create(Category Entity)
        {
            _db.Categories.Add(Entity);
            _db.SaveChanges();
            return Entity;
        }

        public bool Delete(int id)
        {
            Category category = Find(id);
            if (category != null)
            {
                _db.Categories.Remove(category);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public Category Find(int id)
        {
            return _db.Categories.Find(id);
        }

        public List<Category> GetAll()
        {
            return _db.Categories.ToList();
        }

        public Category Update(Category Entity)
        {
            _db.Categories.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }
    }
}
