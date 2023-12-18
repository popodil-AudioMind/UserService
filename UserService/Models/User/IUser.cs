using System.ComponentModel.DataAnnotations;
using UserService.Models;

namespace UserService.Interfaces
{
    public class IUser
    {
        public IUser(User user) 
        {
            id = user.id.ToString();
            displayname = user.displayname;
            email = user.email;
        }
        public IUser() 
        { 
        
        }
        public string id { get; set; }

        [Required]
        public string displayname { get; set; }

        [Required]
        public string email { get; set; }
    }
}
