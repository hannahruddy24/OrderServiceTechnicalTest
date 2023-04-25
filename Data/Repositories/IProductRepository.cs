using Data.Models.DbModels;

namespace Data.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByIds(IEnumerable<string> products);
        Task<bool> DeductStock(Dictionary<string, int> products);
        Task<bool> AddStock(Dictionary<string, int> products);
    }
}
