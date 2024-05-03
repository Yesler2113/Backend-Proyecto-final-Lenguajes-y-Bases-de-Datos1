using Microsoft.EntityFrameworkCore;
using Red_Social_Proyecto.Entities;

namespace Red_Social_Proyecto.Database
{
    public class LogDbContext : DbContext
    {
        public LogDbContext(DbContextOptions<LogDbContext> options) : base(options)
        {
        }

        public DbSet<LogsEntity> Logs { get; set; }
    }
}
