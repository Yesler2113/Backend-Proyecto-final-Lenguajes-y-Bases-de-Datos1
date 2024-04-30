using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Entities;
using System.Reflection.Emit;

namespace Red_Social_Proyecto.Database
{
    public class TodoListDBContext : IdentityDbContext<UsersEntity>
    {
        public TodoListDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("security");

            builder.Entity<UsersEntity>().ToTable("users"); // Cambio aquí
            builder.Entity<IdentityRole>().ToTable("roles");
            builder.Entity<IdentityUserRole<string>>().ToTable("users_roles");
            builder.Entity<IdentityUserClaim<string>>().ToTable("users_claims");
            builder.Entity<IdentityUserLogin<string>>().ToTable("users_logins");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("roles_claims");
            builder.Entity<IdentityUserToken<string>>().ToTable("users_tokens");


            // Configuración de relaciones
            builder.Entity<UsersEntity>()
                .HasMany(u => u.Publications)
                .WithOne(p => p.User)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);  // Considera la acción de cascada apropiada

            builder.Entity<UsersEntity>()
                .HasMany(u => u.Comments)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersEntity>()
                .HasMany(u => u.Interactions)
                .WithOne(i => i.User)
                .HasForeignKey(i => i.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersEntity>()
                .HasMany(u => u.Following)
                .WithOne(f => f.Follower)
                .HasForeignKey(f => f.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UsersEntity>()
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followed)
                .HasForeignKey(f => f.FollowedId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CommentsEntity>()
           .HasOne(c => c.CommentParent) 
           .WithMany(c => c.ChildComments) 
           .HasForeignKey(c => c.CommentParentId) 
           .IsRequired(false) 
           .OnDelete(DeleteBehavior.SetNull);
        }

        //public DbSet<UsersEntity> Users { get; set; }
        public DbSet<FollowEntity> Follows { get; set; }
        public DbSet<PublicationEntity> Publications { get; set; }
        public DbSet<InteractionEntity> Interactions { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<CommentsEntity> Comments { get; set; }
    }
}
