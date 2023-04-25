using Data.Models.Context;
using Data.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger<ProductRepository> _logger;
        private List<Product> _products;
        private readonly OrdersContext _ordersContext;


        public ProductRepository(ILogger<ProductRepository> logger, OrdersContext ordersContext)
        {
             _logger = logger;
             _ordersContext = ordersContext; 
        }     

        public async Task<List<Product>> GetProductsByIds(IEnumerable<string> productIds)
        {
            try
            {
                _products = await _ordersContext.Products.Where(st => productIds.Contains(st.Id.ToString())).ToListAsync();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error trying to fetch products from database. {ex.Message}");
            }
            return _products;
        }
        
        public async Task<bool> DeductStock(Dictionary<string,int> products)
        {
            foreach(var product in _products)
            {
                product.Stock = product.Stock - products.Single(x => x.Key == product.Id.ToString()).Value;
            }

            try
            {
                await _ordersContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to deduct stock from products. {ex.Message}");
                return false;
            }
        }

        public async  Task<bool> AddStock(Dictionary<string, int> products)
        {
            foreach (var product in _products)
            {
                product.Stock = product.Stock + products.Single(x => x.Key == product.Id.ToString()).Value;
            }

            try
            {
                await _ordersContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error trying to add stock to products. {ex.Message}");
                return false;
            }
        }
    }
}
