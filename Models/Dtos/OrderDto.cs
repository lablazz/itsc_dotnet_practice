using System.Collections.Generic;

namespace itsc_dotnet_practice.Models.Dtos;

public class OrderDto
{
    public class Request
    {
        public int UserId { get; set; }
        public List<OrderDetailDto.Request> OrderDetails { get; set; } = new List<OrderDetailDto.Request>();
    }
}
