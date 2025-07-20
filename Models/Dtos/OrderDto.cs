using System.Collections.Generic;

namespace itsc_dotnet_practice.Models.Dtos;

public class OrderDto
{
    public class OrderRequest
    {
        public int UserId { get; set; }
        public List<OrderDetailDto.OrderDetailRequest> OrderDetails { get; set; } = new List<OrderDetailDto.OrderDetailRequest>();
    }
}
