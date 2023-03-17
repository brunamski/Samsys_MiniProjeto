using WebAPI.Infrastructure.Entities;

namespace WebAPI.Infrastructure.DTOs
{
    public class AuthorBooksDTO
    {
        public long authorId { get; set; }

        public string name { get; set; }

        public List<BookDTO> books { get; set; }
    }
}
