using UserService.Context;
using Microsoft.EntityFrameworkCore;

namespace UserService.Services
{
    public static class DatabaseManagementService
    {
        public static void MigrationInitialisation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var database = serviceScope.ServiceProvider.GetService<UserDatabaseContext>().Database;
                var migrations = database.GetMigrations();
                if (migrations == null)
                {
                    database.Migrate();
                }
            }
        }
    }
}
