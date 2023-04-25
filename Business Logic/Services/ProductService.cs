using BusinessLogic.Models;
using Data.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLogic.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository,
            ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<bool> ValidateProducts(List<OrderProduct> products, decimal total)
        {
            //Check that all of the products exist in the db.
            var databaseProducts = await _productRepository.GetProductsByIds(products.Select(x => x.Id.ToString()).ToList());
            if (databaseProducts == null || databaseProducts.Count() < products.Count)
            {
                _logger.LogError("Not all products exist in the database");
                return false;
            }

            //Check that the total price on the order matches up with the price * quantity from the database product item
            Decimal sum = 0;
            foreach(var product in products)
            {
                sum += product.Quantity * databaseProducts.Where(x => x.Id == product.Id).Select(y => y.Price).FirstOrDefault();
            }
            if(sum != total)
            { 
                _logger.LogError("Order total does not match up with the prices found in the database.");
                return false;
            }

            //Check that there is enough stock of each product.
            bool itemsOutOfStock = false ;
            foreach (var product in products)
            {
                if(databaseProducts.Where(x => x.Id == product.Id).Where(x => x.Stock < product.Quantity).Any())
                {
                    _logger.LogError($"Item {product.Id} is out of stock.");
                    itemsOutOfStock = true;
                };
            }

            return itemsOutOfStock ? false : true;
        }

        public async Task<bool> DeductStock(Dictionary<string,int> products)
        {
            return await _productRepository.DeductStock(products);
        }
        
        public async Task<bool> AddStock(Dictionary<string, int> products)
        {
            return await _productRepository.AddStock(products);
        }
    }
}
