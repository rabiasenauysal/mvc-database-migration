using Microsoft.AspNetCore.Mvc;
using odevSon.Interfaces;
using odevSon.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace odevSon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ProductVT _context;

        public ProductController(IProductService productService, ProductVT context)
        {
            _productService = productService;
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Tüm ürünleri listeleme.",
        Description = "Bu endpoint, product tablosundaki tüm ürünlerin listesini döner.")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Belirli bir ürünü getirme. .",
        Description = "Bu endpoint, product tablosundaki seçilen id'ye sahip ürünü listeler.")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Ürün ekleme",
        Description = "Bu endpoint, product tablosuna ürün ekler.")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _productService.AddProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = "Ürün güncelleme",
        Description = "Bu endpoint, product tablosundaki seçilen ürünü günceller.")]
        public async Task<IActionResult> UpdateProduct(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productService.UpdateProductAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = "Ürün silme",
        Description = "Bu endpoint, product tablosundaki belirli id'ye sahip ürünü siler.")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            await _productService.DeleteProductAsync(id);
            return NoContent();
        }

        



    }
}
