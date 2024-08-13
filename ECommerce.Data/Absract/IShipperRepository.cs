using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    // Shipper (nakliyeci) ile ilgili veri erişim işlemlerini tanımlayan arayüz
    public interface IShipperRepository
    {
        // Yeni bir nakliyeci ekler
        Task<int> AddShipperAsync(Shipper shipper);

        // Var olan bir nakliyeciyi günceller
        Task<int> UpdateShipperAsync(Shipper shipper);

        // Belirli bir nakliyeci ID'sine sahip nakliyeciyi siler
        Task<int> DeleteShipperAsync(int shipperId);

        // Belirli bir nakliyeci ID'sine sahip nakliyeciyi getirir
        Task<Shipper> GetShipperByIdAsync(int shipperId);

        // Tüm nakliyecileri getirir
        Task<IEnumerable<Shipper>> GetAllShippersAsync();
    }
}
