// İş mantığı katmanında müşteri servislerini tanımlayan arayüz
using ECommerce.DataAcces.Models;

namespace ECommerce.Business.Absract
{
    // Müşteri ile ilgili iş mantığı işlemlerini tanımlayan arayüz
    public interface ICustomerService
    {
        // Tüm müşterileri asenkron olarak getirir ve sonuç olarak ServiceResult döner
        Task<ServiceResult<IEnumerable<Customer>>> GetAllCustomersAsync();

        // Belirli bir müşteri ID'sine göre müşteri bilgilerini asenkron olarak getirir
        Task<ServiceResult<Customer>> GetCustomerByIdAsync(int customerId);

        // Yeni bir müşteri ekler ve eklenen müşteri ID'si ile sonuç olarak ServiceResult döner
        Task<ServiceResult<int>> AddCustomerAsync(Customer customer);

        // Var olan bir müşteri bilgilerini günceller ve etkilenen satır sayısı ile sonuç olarak ServiceResult döner
        Task<ServiceResult<int>> UpdateCustomerAsync(int customerId, Customer customer);

        // Belirli bir müşteri kaydını siler ve etkilenen satır sayısı ile sonuç olarak ServiceResult döner
        Task<ServiceResult<int>> DeleteCustomerAsync(int customerId, int deletedBy);
    }
}
