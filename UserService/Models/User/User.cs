using System.ComponentModel.DataAnnotations;
using UserService.Interfaces;

namespace UserService.Models
{
    public class User
    {
        public User (IUser user)
        {
            id = Guid.Parse(user.id);
            displayname = user.displayname;
            email = user.email;
        }

        public User() { }

        [Key]
        [Required]
        public Guid id { get; set; }

        [Required]
        public string displayname { get; set; }

        [Required, EmailAddress]
        public string email { get; set; }
    }
}
