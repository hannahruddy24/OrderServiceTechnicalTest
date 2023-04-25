using Data.Models.DbModels;

namespace Data.Repositories
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(Order order);
    }
}
