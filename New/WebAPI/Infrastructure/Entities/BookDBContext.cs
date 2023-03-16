using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;

namespace WebAPI.Entities
{
    public class BookDBContext: DbContext
    {
        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options) 
        {

        }
        public DbSet<Book> Books { get; set; }
    }
}
