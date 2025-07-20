using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace itsc_dotnet_practice.Utilities
{
    public static class MapperUtility
    {
        public static async Task<Order> MapToOrderAsync(OrderDto.OrderRequest request, DbContext dbContext)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));

            var order = new Order(request.UserId)
            {
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            order.OrderDetails = new List<OrderDetail>();

            foreach (var item in request.OrderDetails)
            {
                var product = await dbContext.Set<Product>().FirstOrDefaultAsync(p => p.Id == item.ProductId);

                if (product == null)
                {
                    throw new Exception($"Product with ID {item.ProductId} not found.");
                }

                var detail = new OrderDetail(
                    productId: product.Id,
                    quantity: item.Quantity,
                    price: product.Price,
                    productName: product.Name,
                    productImageUrl: product.ImageUrl,
                    productDescription: product.Description,
                    productCategory: product.Category,
                    orderId: 0,                // will be set by EF Core when saving
                    userId: request.UserId
                );

                order.OrderDetails.Add(detail);
            }

            return order;
        }
    }
}
