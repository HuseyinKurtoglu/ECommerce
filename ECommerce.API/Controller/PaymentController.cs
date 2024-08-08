using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [ApiController] // API denetleyicisi olarak işaretler. Bu, MVC ile uyumlu veri bağlama ve model doğrulama sağlar.
    [Route("api/[controller]")] // URL rotasını tanımlar. `[controller]` yer tutucusu, denetleyicinin adını alır (örneğin, "Payments").
    public class PaymentsController : ControllerBase
    {
        // IPaymentService türünden bir bağımlılığı saklar. Bu, ödeme ile ilgili iş mantığını sağlar.
        private readonly IPaymentService _paymentService;

        // Constructor: IPaymentService nesnesini alır ve _paymentService değişkenine atar.
        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        // Tüm ödemeleri getirir.
        [HttpGet] // Bu yöntem GET isteklerine yanıt verir.
        public async Task<IActionResult> GetAllPayments()
        {
            // Ödemeleri almak için hizmeti çağırır.
            var result = await _paymentService.GetAllPaymentsAsync();
            // Sonucu HTTP durum kodu ile birlikte döner.
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir ödeme bilgilerini getirir.
        [HttpGet("{id}")] // Bu yöntem, URL'de belirli bir id içeren GET isteklerine yanıt verir.
        public async Task<IActionResult> GetPaymentById(int id)
        {
            // Ödeme bilgilerini almak için hizmeti çağırır.
            var result = await _paymentService.GetPaymentByIdAsync(id);
            // Sonucu HTTP durum kodu ile birlikte döner.
            return StatusCode((int)result.StatusCode, result);
        }

        // Yeni bir ödeme ekler.
        [HttpPost] // Bu yöntem POST isteklerine yanıt verir.
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            // Yeni ödeme eklemek için hizmeti çağırır.
            var result = await _paymentService.AddPaymentAsync(payment);
            // Sonucu HTTP durum kodu ile birlikte döner.
            return StatusCode((int)result.StatusCode, result);
        }

        // Var olan bir ödemeyi günceller.
        [HttpPut("{id}")] // Bu yöntem, URL'de belirli bir id içeren PUT isteklerine yanıt verir.
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            // Gönderilen ödeme ID'sinin URL'deki ID ile eşleşip eşleşmediğini kontrol eder.
            if (id != payment.PaymentId) return BadRequest("ID'ler uyuşmuyor.");
            // Ödemeyi güncellemek için hizmeti çağırır.
            var result = await _paymentService.UpdatePaymentAsync(id, payment);
            // Sonucu HTTP durum kodu ile birlikte döner.
            return StatusCode((int)result.StatusCode, result);
        }

        // Belirli bir ödemeyi siler.
        [HttpDelete("{id}")] // Bu yöntem, URL'de belirli bir id içeren DELETE isteklerine yanıt verir.
        public async Task<IActionResult> DeletePayment(int id, [FromQuery] int deletedBy)
        {
            // Ödemeyi silmek için hizmeti çağırır. Silen kullanıcının ID'sini sorgu parametresi olarak alır.
            var result = await _paymentService.DeletePaymentAsync(id, deletedBy);
            // Sonucu HTTP durum kodu ile birlikte döner.
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
