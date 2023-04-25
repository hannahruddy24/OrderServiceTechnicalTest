using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IProductService
    {
        Task<bool> ValidateProducts(List<OrderProduct> products, decimal total);
        Task<bool> DeductStock(Dictionary<string, int> products);
        Task<bool> AddStock(Dictionary<string, int> products);
    }
}
