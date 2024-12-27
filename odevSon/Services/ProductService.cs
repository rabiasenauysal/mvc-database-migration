using odevSon.Interfaces;
using odevSon.Models;
using ILogger = odevSon.Interfaces.ILogger;
using odevSon.Services;
using odevSon.Models.ProductOrderManagement.Models;

namespace odevSon.Services
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Order> _orderRepository;



        private readonly ILogger _logger;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Order> orderRepository,
            ILogger logger)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _logger = logger;
        }


        public async Task AddProductAsync(Product product)
        {
            await _productRepository.AddAsync(product);
            _logger.Log($"Product added: {product.Name}");
        }


        public ProductService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }



        public async Task<List<Product>> GetAllProductsAsync() => await _productRepository.GetAllAsync();

        public async Task<Product> GetProductByIdAsync(int id) => await _productRepository.GetByIdAsync(id);

        //public async Task AddProductAsync(Product product) => await _productRepository.AddAsync(product);

        public async Task UpdateProductAsync(Product product) => await _productRepository.UpdateAsync(product);

        public async Task DeleteProductAsync(int id) => await _productRepository.DeleteAsync(id);

        public async Task<List<Product>> GetExpensiveProductsAsync()
        {
            return await _productRepository.FindAsync(p => p.Price > 500);
        }

        public async Task<int> GetTotalStockAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Sum(p => p.Stock);
        }

        public async Task<Product> GetMostStockedProductAsync()
        {
            return await _productRepository
                .FindAsync(p => true)
                .ContinueWith(t => t.Result.OrderByDescending(p => p.Stock).FirstOrDefault());
        }

        



        public async Task<Product> GetMostOrderedProductAsync()
        {
            var orders = await _orderRepository.GetAllAsync();

            var grouped = orders.GroupBy(o => o.ProductId)
                                .Select(g => new { ProductId = g.Key, TotalOrders = g.Count() })
                                .OrderByDescending(g => g.TotalOrders)
                                .FirstOrDefault();

            return grouped != null ? await _productRepository.GetByIdAsync(grouped.ProductId) : null;
        }



    }
}
