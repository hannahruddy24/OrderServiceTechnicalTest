using BusinessLogic.Models;
using Data.Models.DbModels;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace BusinessLogic.Services
{
    public class OrderService : IOrderService
    {
        private readonly ILogger<OrderService> _logger;
        private readonly IProductService _productService;
        private readonly IOrderRepository _orderRepository;

        public OrderService(ILogger<OrderService> logger,
            IProductService productService,
            IOrderRepository orderRepository)
        {
            _logger = logger;
            _productService = productService;
            _orderRepository = orderRepository;
        }

        public async Task<bool> ValidateOrder(OrderDTO order)
        {
            if (String.IsNullOrWhiteSpace(order.Order.Id.ToString()))
            {
                _logger.LogError("Invalid Order - No order id attached.");
                return false;
            }

            return await _productService.ValidateProducts(order.Products, order.Order.Total);
        }

        public async Task<bool> ProcessOrder(OrderDTO order)
        {
            var productQuantities = new Dictionary<string, int>();
            foreach (var product in order.Products)
            {
                productQuantities.Add(product.Id.ToString(), product.Quantity);
            }

            if (!await _productService.DeductStock(productQuantities)) return false;
            
            var orderModel = GenerateOrderEntry(order);
            if(!await _orderRepository.AddOrder(orderModel))
            {
                await _productService.AddStock(productQuantities);
                return false;
            };

            return true;
        }

        private Order GenerateOrderEntry(OrderDTO order)
        {
            return new Order()
            {
                Id = order.Order.Id,
                Total = order.Order.Total,
                CustomerId = order.Customer.Id.ToString(),
                CustomerName = order.Customer.Name,
                ProductIds = String.Join(",", order.Products.Select(x => x.Id.ToString()))
            };
        }
    }
        
}
