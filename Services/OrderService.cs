using itsc_dotnet_practice.Models;
using itsc_dotnet_practice.Repositories;

namespace itsc_dotnet_practice.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Order> CreateOrderAsync(Order order)
        {
            return await _repository.CreateAsync(order);
        }

        public async Task<bool> DeleteOrderAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
