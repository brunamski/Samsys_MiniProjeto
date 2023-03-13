using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities
{
    public class Book
    {
        [Key]
        public string isbn { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string author { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal price { get; set; }
    }
}
