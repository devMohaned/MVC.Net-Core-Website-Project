namespace Final_Project.Services.SubCategoryServiceFolder
{
    public class SubCategoryService : ISubCategoryServicecs
    {
        private readonly ApplicationDbContext _db;

        public SubCategoryService(ApplicationDbContext db)
        {
            _db = db;
        }

        public SubCategory Create(SubCategory Entity)
        {
            _db.SubCategories.Add(Entity);
            _db.SaveChanges();
            return Entity;
        }

        public bool Delete(int id)
        {
            SubCategory sub = Find(id);
            if (sub!=null)
            {
                _db.SubCategories.Remove(sub);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public SubCategory Find(int id)
        {
            return _db.SubCategories.Find(id);
        }

        public List<SubCategory> GetAll()
        {
            return _db.SubCategories.ToList();
        }

        public SubCategory Update(SubCategory Entity)
        {
            _db.SubCategories.Update(Entity);
            _db.SaveChanges();
            return Entity;
        }
    }
}
