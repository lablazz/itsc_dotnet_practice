using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itsc_dotnet_practice.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }

        [Required]
        public string ProductName { get; set; } = "";

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        // Navigation property
        public Order Order { get; set; } = null!;
    }
}
