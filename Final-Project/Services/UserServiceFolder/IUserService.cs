using Final_Project.Models;

namespace Final_Project.Services.UserServiceFolder
{
    public interface IUserService : IGeneralDataService<User>, ISingleDataService<User>
    {

        public User GetbyEmail(string Email);
        public User Login(string UserName, string Password);

    }
}
