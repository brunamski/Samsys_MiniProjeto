using Microsoft.EntityFrameworkCore;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Entities
{
    public class AppDBContext: DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Author>(t =>
            {
                t.Property(s => s.authorId).UseIdentityColumn();

                t.HasKey(r => r.authorId);
            }); 
        }
    }
}
