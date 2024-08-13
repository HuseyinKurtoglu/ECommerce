using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Absract
{
    public interface IPaymentRepository
    {
        // Tüm ödemeleri asenkron olarak getirir
        Task<IEnumerable<Payment>> GetAllPaymentsAsync();

        // Belirli bir ödemeyi ID'ye göre asenkron olarak getirir
        Task<Payment> GetPaymentByIdAsync(int paymentId);

        // Yeni bir ödemeyi asenkron olarak ekler ve eklenen ödemenin ID'sini döner
        Task<int> AddPaymentAsync(Payment payment);

        // Mevcut bir ödemeyi asenkron olarak günceller
        Task<int> UpdatePaymentAsync(int paymentId, Payment payment);

        // Bir ödemeyi asenkron olarak siler (yumuşak silme)
        Task<int> DeletePaymentAsync(int paymentId, int deletedBy);
    }
}
