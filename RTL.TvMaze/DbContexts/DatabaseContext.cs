using Microsoft.EntityFrameworkCore;
using RTL.TvMaze.Models;

namespace RTL.TvMaze.DbContexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public virtual DbSet<Show> Shows { get; set; }
        public virtual DbSet<Cast> Casts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cast>();
            modelBuilder.Entity<Show>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
