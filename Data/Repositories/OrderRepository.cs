using Data.Models.Context;
using Data.Models.DbModels;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;
        private readonly OrdersContext _ordersContext;

        public OrderRepository(ILogger<OrderRepository> logger, OrdersContext ordersContext)
        {
            _logger = logger;
            _ordersContext = ordersContext;
        }

        public async Task<bool> AddOrder(Order order)
        {
            try
            {
                _ordersContext.Orders.Add(order);
                await _ordersContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding the order to the database. {ex.Message}");
                return false;
            }
        }
    }
}
