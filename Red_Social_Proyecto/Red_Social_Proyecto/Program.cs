using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Red_Social_Proyecto.Entities;
using todo_list_backend;
using todo_list_backend.Database;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);


var app = builder.Build();

startup.Configure(app, app.Environment);

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();

    try
    {
        var userManager = services.GetRequiredService<UserManager<UsersEntity>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await TodoListDbSeeder.LoadDataAsync(userManager, roleManager, loggerFactory);
    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error al inicializar datos. ");
    }
}

app.Run();