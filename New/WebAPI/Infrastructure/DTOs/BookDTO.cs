using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI.Infrastructure.Entities;

namespace WebAPI.Infrastructure.DTOs
{
    public class BookDTO
    {
        public string isbn { get; set; }
   
        public string name { get; set; }

        public long authorId { get; set; }

        public decimal price { get; set; }

        public string author { get; set; }
    }
}