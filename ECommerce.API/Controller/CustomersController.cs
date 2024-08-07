using ECommerce.Business.Absract; 
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc; 

// API denetleyicisi için isim alanı
namespace ECommerce.API.Controller
{
    // API denetleyici sınıfı
    [ApiController] // Bu sınıfın bir API denetleyicisi olduğunu belirtir
    [Route("api/[controller]")] // API yolu için denetleyici adını belirtir (örneğin: /api/customers)
    public class CustomersController : ControllerBase
    {
        // ICustomerService bağımlılığı
        private readonly ICustomerService _customerService;

        // Constructor, ICustomerService'yi bağımlılık olarak alır
        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService; // Bağımlılığı sınıf üyesine atar
        }

        // Tüm müşterileri getirir
        [HttpGet] // GET isteği ile bu metodu çağırır
        public async Task<IActionResult> GetAllCustomers()
        {
            var result = await _customerService.GetAllCustomersAsync(); // Servis aracılığıyla tüm müşterileri getirir
            return result.Success ? Ok(result.Data) : BadRequest(result.Message); // Sonuç başarılıysa HTTP 200, başarısızsa HTTP 400 döner
        }

        // ID ile müşteri getirir
        [HttpGet("{id}")] // GET isteği ile belirli bir ID'yi içerir
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id); // Servis aracılığıyla belirli müşteri ID'sini getirir
            return result.Success ? Ok(result.Data) : NotFound(result.Message); // Sonuç başarılıysa HTTP 200, müşteri bulunamazsa HTTP 404 döner
        }

        // Yeni müşteri ekler
        [HttpPost] // POST isteği ile bu metodu çağırır
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            var result = await _customerService.AddCustomerAsync(customer); // Servis aracılığıyla yeni müşteri ekler
            return result.Success ? CreatedAtAction(nameof(GetCustomerById), new { id = result.Data }, result.Data) : BadRequest(result.Message); // Başarıyla eklenirse HTTP 201 ve eklenen müşteri bilgilerini döner, başarısızsa HTTP 400 döner
        }

        // Müşteri bilgilerini günceller
        [HttpPut("{id}")] // PUT isteği ile belirli bir ID'yi içerir
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId) return BadRequest("ID uyuşmuyor"); // ID'ler uyuşmazsa HTTP 400 döner
            var result = await _customerService.UpdateCustomerAsync(id, customer); // Servis aracılığıyla müşteri bilgilerini günceller
            return result.Success ? NoContent() : BadRequest(result.Message); // Başarıyla güncellenirse HTTP 204 döner, başarısızsa HTTP 400 döner
        }

        // Müşteri siler
        [HttpDelete("{id}")] // DELETE isteği ile belirli bir ID'yi içerir
        public async Task<IActionResult> DeleteCustomer(int id, [FromQuery] int deletedBy)
        {
            var result = await _customerService.DeleteCustomerAsync(id, deletedBy); // Servis aracılığıyla müşteri siler
            return result.Success ? NoContent() : BadRequest(result.Message); // Başarıyla silinirse HTTP 204 döner, başarısızsa HTTP 400 döner
        }
    }
}
