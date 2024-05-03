using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Red_Social_Proyecto.Database;
using Red_Social_Proyecto.Entities;
using Red_Social_Proyecto.ResetPassword;
using Red_Social_Proyecto.ResetPassword.Service;
using Red_Social_Proyecto.Services;
using Red_Social_Proyecto.Services.Interfaces;
using Red_Social_Proyecto.Services.LogsService;
using Red_Social_Proyecto.Services.LogsService.Interface;
using Red_Social_Proyecto.SignalRConnect;
using System.Text;
using todo_list_backend.Database;
using todo_list_backend.Services;

namespace todo_list_backend
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSignalR();
            // Add DbContext

            
            services.AddDbContext<TodoListDBContext>(options =>
                           options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<LogDbContext>(options =>
                                      options.UseNpgsql(Configuration.GetConnectionString("LogsConnection")));

            // Add Custom Services

            //services.AddTransient<ITasksService, TasksService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<ICommentsService, CommentsService>();
            services.AddTransient<IFollowService, FollowService>();
            services.AddTransient<IInteractionService, InteractionService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ILogService, LogService>();

            // Add Automapper Service
            services.AddAutoMapper(typeof(Startup));

            services.AddScoped<LogService>();

            services.AddHttpContextAccessor();

           

            services.AddIdentity<UsersEntity, IdentityRole>(options =>
            {
                // Configuraciones de validación de usuario
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                // Configuraciones de validación de contraseña
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings
                options.User.RequireUniqueEmail = true;

                // Sign-in settings
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
            }).AddEntityFrameworkStores<TodoListDBContext>()
                .AddDefaultTokenProviders();


            //Add Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["JWT:ValidAudience"],
                    ValidIssuer = Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JWT:Secret"]!))
                };
            });



            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins(Configuration["FrontendURL"]!).AllowAnyHeader().AllowAnyMethod();
                });
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<PublicationHub>("/publicationHub");
                endpoints.MapHub<CommentsHub>("/commentsHub");
                endpoints.MapHub<InteractionHub>("/interactionHub");
                endpoints.MapHub<FollowHub>("/followHub");
            });
        }
    }
}
