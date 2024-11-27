using Microsoft.EntityFrameworkCore;
using ProjectIko.Db.Configuration;
using ProjectIko.Models;

namespace ProjectIko.Db
{
    public class AppContext : DbContext
    {
        public AppContext() : base() { }

        public AppContext(DbContextOptions<AppContext> options) : base(options) { }

        public virtual DbSet<Model> Models { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ModelConfiguration());
        }
    }
}
