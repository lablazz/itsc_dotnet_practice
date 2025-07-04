using System.ComponentModel.DataAnnotations.Schema;

namespace itsc_dotnet_practice.Models;

[Table("Products")] // Ensure it uses exact name
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}