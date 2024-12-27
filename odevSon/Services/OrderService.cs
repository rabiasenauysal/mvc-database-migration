using odevSon.Interfaces;
using odevSon.Models;
using odevSon.Models.ProductOrderManagement.Models;

namespace odevSon.Services
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Product> _productRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<Product> productRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        }


        public async Task<List<Order>> GetAllOrdersAsync() => await _orderRepository.GetAllAsync();

        public async Task<Order> GetOrderByIdAsync(int id) => await _orderRepository.GetByIdAsync(id);

        public async Task AddOrderAsync(Order order)
        {
            // Product kontrolü
            var product = await _productRepository.GetByIdAsync(order.ProductId);
            if (product == null)
                throw new ArgumentException("Invalid ProductId. The product does not exist.");

            // Stok kontrolü
            if (product.Stock < order.Quantity)
                throw new ArgumentException("Insufficient stock for the product.");

            // Product bağlama
            order.Product = product;

            // Order ekleme
            await _orderRepository.AddAsync(order);
        }



        public async Task UpdateOrderAsync(Order order)
        {
            // Product kontrolü
            var product = await _productRepository.GetByIdAsync(order.ProductId);
            if (product == null)
                throw new ArgumentException("Invalid ProductId. The product does not exist.");

            // Güncelleme işlemi
            await _orderRepository.UpdateAsync(order);
        }


        public async Task DeleteOrderAsync(int id) => await _orderRepository.DeleteAsync(id);

        public async Task<decimal> CalculateTotalOrderAmountAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Sum(o => o.Quantity * o.Product.Price);
        }

        public async Task<List<Order>> GetOrdersAfterDateAsync(DateTime date)
        {
            return await _orderRepository.FindAsync(o => o.OrderDate > date);
        }

        public async Task<decimal> GetTotalOrderAmountAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders.Sum(o => o.Quantity * o.Product.Price);
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
