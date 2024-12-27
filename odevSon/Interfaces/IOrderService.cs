using odevSon.Models;
using odevSon.Models.ProductOrderManagement.Models;

namespace odevSon.Interfaces
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task<decimal> CalculateTotalOrderAmountAsync();

        Task<List<Order>> GetOrdersAfterDateAsync(DateTime date);
        Task<decimal> GetTotalOrderAmountAsync();

        Task<Product> GetMostOrderedProductAsync();

    }
}
