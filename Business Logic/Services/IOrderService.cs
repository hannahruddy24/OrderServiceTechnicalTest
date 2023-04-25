
using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public interface IOrderService
    {
        Task<bool> ValidateOrder(OrderDTO order);
        Task<bool> ProcessOrder(OrderDTO order);
    }
}
