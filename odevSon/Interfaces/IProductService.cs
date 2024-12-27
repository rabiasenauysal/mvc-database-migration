using odevSon.Models;

namespace odevSon.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);

        Task<Product> GetMostOrderedProductAsync();

        Task<List<Product>> GetExpensiveProductsAsync();
        Task<int> GetTotalStockAsync();

    }
}
