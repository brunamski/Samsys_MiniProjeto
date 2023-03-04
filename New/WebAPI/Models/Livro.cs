using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Livro
    {
        [Key]
        public string isbn { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string name { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string author { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal preco { get; set; }
    }
}
