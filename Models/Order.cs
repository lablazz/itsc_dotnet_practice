using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace itsc_dotnet_practice.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; } // Foreign key to User

        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!; // Navigation property

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
