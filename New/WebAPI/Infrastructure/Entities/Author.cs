using System.ComponentModel.DataAnnotations;
using WebAPI.Entities;

namespace WebAPI.Infrastructure.Entities
{
    public class Author
    {
        public long Id { get; set; }
        [Required] public string Name { get; set; }

    }
}
