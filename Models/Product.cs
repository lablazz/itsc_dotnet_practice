using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itsc_dotnet_practice.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required] public string Name { get; set; } = "";
        [Required] public string Description { get; set; } = "";
        [Required] public decimal Price { get; set; }
        [Required] public int Stock { get; set; }
        public string Category { get; set; } = "";
        public string ImageUrl { get; set; } = "";
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
