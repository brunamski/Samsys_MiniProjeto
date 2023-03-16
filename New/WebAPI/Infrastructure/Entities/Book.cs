using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Infrastructure.Entities
{
    public class Book
    {
        [Key]
        public string isbn { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

        public long authorId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal price { get; set; }
    }
}
