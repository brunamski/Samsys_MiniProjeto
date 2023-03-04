using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models
{
    public class BookDBContext: DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options) 
        {

        }

        public DbSet<Livro> Livros { get; set; }

    }
}
