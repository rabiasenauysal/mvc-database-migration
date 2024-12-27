using Microsoft.AspNetCore.Mvc;
using odevSon.Interfaces;
using odevSon.Models;
using odevSon.Models.ProductOrderManagement.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace odevSon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ProductVT _context;
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService,ProductVT context)
        {
            _orderService = orderService;
            _context = context;
        }

        [HttpGet]
        [SwaggerOperation(
        Summary = "Tüm ürünleri listele",
        Description = "Bu endpoint, orders tablosundaki tüm ürünleri listeler.")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = " Belirli bir ürünü getirme. ",
        Description = "Bu endpoint, orders tablosundaki seçili id'ye sahip ürünü listeler.")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "ürün ekleme",
        Description = "Bu endpoint, orders tablosuna ürün ekler.")]


        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Product kontrolü servis içinde yapılacak
            await _orderService.AddOrderAsync(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }


        [HttpPut("{id}")]
        [SwaggerOperation(
        Summary = " Belirli bir ürünü güncelleme ",
        Description = "Bu endpoint, orders tablosundaki seçili id'ye sahip ürünü günceller")]
        public async Task<IActionResult> UpdateOrder(int id, [FromBody] Order order)
        {
            if (id != order.Id)
                return BadRequest("Route id and order id do not match.");

            try
            {
                await _orderService.UpdateOrderAsync(order);
                return Ok("Order updated successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpDelete("{id}")]
        [SwaggerOperation(
        Summary = " Ürün silme.",
        Description = "Bu endpoint, orders tablosundaki seçili id'ye sahip ürünü siler.")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }

        

    }
}
