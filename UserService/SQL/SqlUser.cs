using UserService.Context;
using UserService.Data;
using UserService.Interfaces;
using UserService.Models;

namespace UserService.SQL
{
    public class SqlUser : ISqlUser
    {
        private readonly UserDatabaseContext _userContext;
        public SqlUser(UserDatabaseContext userContext)
        {
            this._userContext = userContext;
        }
        public User AddUser(User user)
        {
            IUser? emailUser = _userContext.Users.FirstOrDefault(x => x.email == user.email);
            IUser? guidUser = _userContext.Users.FirstOrDefault(x => x.id == user.id.ToString());
            if (emailUser == null && guidUser == null)
            {
                /*Guid id = Guid.NewGuid();
                while (existingUser.id == id)
                {
                    id = Guid.NewGuid();
                }
                existingUser.id = id;*/
                _userContext.Users.Add(new IUser(user));
                _userContext.SaveChanges();
                return user;
            }
            return null;
        }

        public bool DeleteUser(string userId)
        {
            IUser? existingUser = _userContext.Users.FirstOrDefault(x => x.id == userId);
            if (existingUser != null)
            {
                _userContext.Users.Remove(existingUser);
                _userContext.SaveChanges();
                return true;
            }
            return false;
        }

        public bool DeleteUserById(string userId)
        {
            IUser? existingUser = _userContext.Users.FirstOrDefault(x => x.id == userId);
            if (existingUser != null)
            {
                _userContext.Users.Remove(existingUser);
                _userContext.SaveChanges();
                return true;
            }
            return false;
        }

        public User GetUser(string userId)
        {
            IUser? existingUser = _userContext.Users.FirstOrDefault(x => x.id == userId);
            if (existingUser != null)
            {
                return new User(existingUser);
            }
            return null;
        }

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            foreach (IUser iuser in _userContext.Users.ToList())
            {
                users.Add(new User(iuser));
            }
            return users;
        }

        public User UpdateUser(User user)
        {
            IUser? existingUser = _userContext.Users.FirstOrDefault(x => x.email == user.email);
            if (existingUser != null)
            {
                existingUser.displayname = user.displayname;

                _userContext.Users.Update(existingUser);
                _userContext.SaveChanges();
                return new User(existingUser);
            }
            return null;
        }
    }
}
