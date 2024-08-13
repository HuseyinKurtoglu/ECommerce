using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ECommerce.DataAcces.Absract
{
    // Müşteri veritabanı işlemleri için gerekli metotları tanımlayan arayüz
    public interface ICustomerRepository
    {
        // Tüm müşterileri asenkron olarak getirir
        Task<IEnumerable<Customer>> GetAllCustomersAsync();

        // Belirli bir müşteri ID'sine sahip müşteriyi asenkron olarak getirir
        Task<Customer> GetCustomerByIdAsync(int customerId);

        // Yeni bir müşteri ekler ve eklenen müşteri için oluşturulan ID'yi döner
        Task<int> AddCustomerAsync(Customer customer);

        // Var olan bir müşteriyi günceller ve güncellenen satır sayısını döner
        Task<int> UpdateCustomerAsync(Customer customer);

        // Belirli bir müşteri ID'sine sahip müşteriyi siler ve silinen satır sayısını döner
        Task<int> DeleteCustomerAsync(int customerId, int deletedBy);
    }
}
