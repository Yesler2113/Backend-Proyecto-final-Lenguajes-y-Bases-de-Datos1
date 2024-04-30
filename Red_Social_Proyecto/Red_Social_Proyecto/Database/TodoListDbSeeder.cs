using Microsoft.AspNetCore.Identity;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Entities;

namespace todo_list_backend.Database
{
    public static class TodoListDbSeeder
    {
        public static async Task LoadDataAsync(
            UserManager<UsersEntity> userManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                if (!roleManager.Roles.Any())
                {
                    await roleManager.CreateAsync(new IdentityRole("USER"));
                    await roleManager.CreateAsync(new IdentityRole("ADMIN"));

                }

                if (!userManager.Users.Any())
                {
                    var userAdmin = new UsersEntity
                    {
                        UserName = "jperez@me.com",
                        Email = "jperez@me.com"
                    };
                    await userManager.CreateAsync(userAdmin, "Temporal001*");
                    await userManager.AddToRoleAsync(userAdmin, "ADMIN");

                    var normalUser = new UsersEntity
                    {
                        UserName = "mperez@me.com",
                        Email = "mperez@me.com"
                    };
                    await userManager.CreateAsync(normalUser, "Temporal002*");
                    await userManager.AddToRoleAsync(normalUser, "USER");
                }

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<TodoListDBContext>();
                logger.LogError(e.Message);
            }
        }
    }
}

