using Final_Project.Models;

namespace Final_Project.Services.UserServiceFolder
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _db;

        public UserService(ApplicationDbContext db)
        {
            _db = db;
        }

        public User Create(User Entity)
        {
            if (GetbyEmail(Entity.Email) == null)
            {
                _db.Users.Add(Entity);
                _db.SaveChanges();
            }
            return Entity;
        }

        public bool Delete(int id)
        {
            User user = Find(id);
            if (user != null)
            {
                _db.Remove(user);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public User Find(int id)
        {
            return _db.Users.Find(id);
        }

        public List<User> GetAll()
        {
            return _db.Users.ToList();
        }

        public User GetbyEmail(string Email)
        {
            return _db.Users.FirstOrDefault(x => x.Email == Email);
        }

        public User Login(string UserName, string Password)
        {
            return new User();
        }

        public User Update(User Entity)
        {
            try
            {
                _db.Update(Entity);
                _db.SaveChanges();
            }
            catch (Exception)
            {

            }
            return Entity;
        }
    }
}
