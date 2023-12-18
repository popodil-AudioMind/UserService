using UserService.Models;
namespace UserService.Data
{
    public interface ISqlUser
    {
        User GetUser(string email);
        List<User> GetUsers();
        User AddUser(User user);
        bool DeleteUser(string Email);
        User UpdateUser(User user);
    }
}
