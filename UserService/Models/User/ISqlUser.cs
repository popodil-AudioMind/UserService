using UserService.Models;
namespace UserService.Data
{
    public interface ISqlUser
    {
        User GetUser(string userId);
        List<User> GetUsers();
        User AddUser(User user);
        bool DeleteUser(string userId);
        bool DeleteUserById(string userId);
        User UpdateUser(User user);
    }
}
