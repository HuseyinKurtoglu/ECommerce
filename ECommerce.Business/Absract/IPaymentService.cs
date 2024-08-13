using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ECommerce.Business.Absract
{
    // IPaymentService arayüzü, ödeme işlemleri için gerekli servis yöntemlerini tanımlar.
    public interface IPaymentService
    {
        // Tüm ödemeleri asenkron olarak getirir.
        // Bu yöntem, ödemeleri içeren bir koleksiyon döner ve işlem sonucu ile birlikte HTTP durum kodunu içerir.
        Task<ServiceResult<IEnumerable<Payment>>> GetAllPaymentsAsync();

        // Belirli bir ödeme ID'sine sahip ödemeyi asenkron olarak getirir.
        // Bu yöntem, tek bir ödeme nesnesini döner ve işlem sonucu ile birlikte HTTP durum kodunu içerir.
        Task<ServiceResult<Payment>> GetPaymentByIdAsync(int paymentId);

        // Yeni bir ödeme ekler.
        // Bu yöntem, eklenen ödemeye ait ID'yi döner ve işlem sonucu ile birlikte HTTP durum kodunu içerir.
        Task<ServiceResult<int>> AddPaymentAsync(Payment payment);

        // Var olan bir ödemeyi günceller.
        // Bu yöntem, güncellenen ödemeye ait etkilenen satır sayısını döner ve işlem sonucu ile birlikte HTTP durum kodunu içerir.
        Task<ServiceResult<int>> UpdatePaymentAsync(int paymentId, Payment payment);

        // Belirli bir ödeme ID'sine sahip ödemeyi siler.
        // Bu yöntem, silinen ödemeye ait etkilenen satır sayısını döner ve işlem sonucu ile birlikte HTTP durum kodunu içerir.
        Task<ServiceResult<int>> DeletePaymentAsync(int paymentId, int deletedBy);
    }
}