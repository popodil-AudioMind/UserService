using Microsoft.EntityFrameworkCore;
using UserService.Interfaces;

namespace UserService.Context
{
    public class UserDatabaseContext : DbContext
    {
        public UserDatabaseContext(DbContextOptions<UserDatabaseContext> options) : base(options)
        { 
        
        }
        public DbSet<IUser> Users { get; set; }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<IUser>()
                .Property(e => e.id)
                .HasConversion(
                    v => v.ToString(),
                    v => Guid.Parse(v));
        }*/
    }

}
