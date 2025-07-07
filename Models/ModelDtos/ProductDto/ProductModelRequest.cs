namespace itsc_dotnet_practice.Models.ModelDtos.ProductDto
{
    public class ProductModelRequest
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
