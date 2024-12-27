using Microsoft.AspNetCore.Mvc; // ASP.NET Core MVC altyapısını kullanmak için gerekli.
using Microsoft.EntityFrameworkCore; // Entity Framework Core'u kullanarak veri tabanı işlemleri yapmak için gerekli.
using odevSon.Interfaces; // Projeye ait tanımlanmış servis arayüzlerini kullanmak için.
using odevSon.Models; // Projeye ait model sınıflarını kullanmak için.
using Swashbuckle.AspNetCore.Annotations; // Swagger dokümantasyonu için açıklamalar eklemeye olanak tanır.

namespace odevSon.Controllers // Projede bu controller'ın ait olduğu ad alanını belirtir.
{
    [Route("api/[controller]")] //Bu, URL rotasını tanımlar. Örneğin, bu controller için /api/Customer adresi kullanılır.
    [ApiController] // Bu sınıfın bir API Controller olduğunu ve otomatik model validasyonu gibi özelliklerden faydalanacağını belirtir.
    public class CustomerController : ControllerBase // Controller sınıfının daha hafif bir versiyonudur. Görünüm (View) gerektirmeyen API projelerinde kullanılır..
    {
        private readonly ICustomerService _customerService; // Müşteri işlemleriyle ilgili iş mantığını barındıran servis arayüzü.Dependency Injection ile dışarıdan alınır
        private readonly DbContext _dbContext; // Veri tabanı işlemlerini yönetmek için Entity Framework Core'un temel sınıfı.

        // Constructor (yapıcı metod) - Bu controller oluşturulduğunda dışarıdan servis ve veri tabanı bağlamını alır.
        //Neden Kullanılır? Dependency Injection(DI) uygulamak için.

        public CustomerController(ICustomerService customerService, ProductVT context)
        {
            _customerService = customerService; // Gelen müşteri servisini private değişkene atar.
            _dbContext = context; // Gelen veri tabanı bağlamını private değişkene atar.
        }

        // GET: api/Customer - Tüm müşterileri listeleyen metod.
        [HttpGet] // HTTP GET isteğiyle çalıştığını belirtir.
        [SwaggerOperation( // Swagger'da bu API endpoint için açıklama eklemek amacıyla kullanılır.
        Summary = " Tüm müşterileri listeler." // Swagger'da metodun kısa açıklaması olarak görünür.
        )]
        public async Task<IActionResult> GetAllCustomers() // Asenkron olarak çalışır ve HTTP yanıtı döner.
        {
            var customers = await _customerService.GetAllCustomersAsync(); // Müşteri listesini almak için servis metodunu çağırır.
            return Ok(customers); // HTTP 200 (OK) yanıtı ve müşteri listesini döner.
        }

        // POST: api/Customer - Yeni bir müşteri ekleyen metod.
        [HttpPost] // HTTP POST isteğiyle çalıştığını belirtir.
        [SwaggerOperation( // Swagger'da bu API endpoint için açıklama eklemek amacıyla kullanılır.
        Summary = "Müşteri ekleme." // Swagger'da metodun kısa açıklaması olarak görünür.
        )]
        public async Task<IActionResult> AddCustomer(Customer customer) // Yeni müşteri verisi alır ve HTTP yanıtı döner.
        {
            try // Hata yönetimi için try-catch bloğu kullanılır.
            {
                if (!ModelState.IsValid) // Gönderilen müşteri modelinin validasyon kurallarına uyup uymadığını kontrol eder.
                    return BadRequest(ModelState); // Validasyon hatası varsa HTTP 400 (Bad Request) döner ve hataları içerir.

                await _customerService.AddCustomerAsync(customer); // Yeni müşteri eklemek için servis metodunu çağırır.
                return CreatedAtAction(nameof(GetAllCustomers), new { id = customer.Id }, customer);
                // HTTP 201 (Created) yanıtı döner, yeni müşterinin verisini ve oluşturulduğu URL'yi içerir.
            }
            catch (Exception ex) // Herhangi bir hata yakalanırsa bu blok çalışır.
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
                // HTTP 500 (Internal Server Error) yanıtı ve hata mesajını döner.
            }
        }
    }
}
