using WebAPI.Infrastructure.DTOs;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Infrastructure.DTOs
{
    public class AddBookDTO
    {
        public string isbn { get; set; }
        public string name { get; set; }
        public long authorId { get; set; }
        public decimal price { get; set; }
    }

}
