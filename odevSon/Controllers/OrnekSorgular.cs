using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using odevSon.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace odevSon.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrnekSorgular : ControllerBase
    {
        private readonly ProductVT _dbContext;

        public OrnekSorgular(ProductVT dbContext)
        {
            _dbContext = dbContext;
        }

        // Expensive Products: Fiyatı 500 TL üstü olan ürünler
        [HttpGet("expensive")]
        [SwaggerOperation(
        Summary = "500 TL'nin üzerindeki ürünleri getirir",
        Description = "Bu endpoint, fiyatı 500 TL üzerinde olan tüm ürünleri döner."
    )]
        public async Task<IActionResult> GetExpensiveProducts()
        {
            var expensiveProducts = await _dbContext.Products
                .Where(p => p.Price > 500)
                .ToListAsync();

            return Ok(expensiveProducts);
        }

        // Most Ordered Products: En çok sipariş edilen ürünler
        [HttpGet("mostordered")]
        [SwaggerOperation(
        Summary = "En çok sipariş edilen ürünler"
    )]
        public async Task<IActionResult> GetMostOrderedProducts()
        {
            var mostOrderedProducts = await _dbContext.Orders
                .GroupBy(o => o.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    OrderCount = g.Count()
                })
                .OrderByDescending(x => x.OrderCount)
                .Take(5) // İlk 5 ürün
                .ToListAsync();

            return Ok(mostOrderedProducts);
        }

        // Total Stock: Toplam stok miktarı
        [HttpGet("totalstock")]
        [SwaggerOperation(
        Summary = "Tüm ürünlerin toplam stok miktarını getirir",
        Description = "Bu endpoint, tüm ürünlerin toplam stok miktarını hesaplar ve döner."
    )]
        public async Task<IActionResult> GetTotalStock()
        {
            var totalStock = await _dbContext.Products
                .SumAsync(p => p.Stock);

            return Ok(new { TotalStock = totalStock });
        }

        // Orders After Date: Belirli bir tarihten sonraki siparişler
        [HttpGet("afterdate/{date}")]
        [SwaggerOperation(
        Summary = "Belirli bir tarihten sonraki siparişleri getirir",
        Description = "Bu endpoint, belirttiğiniz tarihten sonra verilen siparişlerin bir listesini döner."
    )]
        public async Task<IActionResult> GetOrdersAfterDate(DateTime date)
        {
            var orders = await _dbContext.Orders
                .Where(o => o.OrderDate > date)
                .ToListAsync();

            return Ok(orders);
        }
    }
}