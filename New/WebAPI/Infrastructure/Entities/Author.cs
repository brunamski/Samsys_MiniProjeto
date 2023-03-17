using System.ComponentModel.DataAnnotations;

namespace WebAPI.Infrastructure.Entities
{
    public class Author
    {
        [Key]
        public long authorId { get; set; }

        public string name { get; set; }

        public List<Book> books { get; set; }
    }
}
