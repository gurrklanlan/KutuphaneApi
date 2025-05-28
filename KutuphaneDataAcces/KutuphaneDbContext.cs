using System.Reflection;
using KutuphaneCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace KutuphaneDataAcces
{
    public class KutuphaneDbContext(DbContextOptions<KutuphaneDbContext> options):DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }


        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }


    }
}
