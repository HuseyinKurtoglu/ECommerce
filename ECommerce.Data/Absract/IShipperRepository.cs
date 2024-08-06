using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    public interface IShipperRepository
    {
        Task<int> AddShipperAsync(Shipper shipper);
        Task<int> UpdateShipperAsync(Shipper shipper);
        Task<int> DeleteShipperAsync(int shipperId);
        Task<Shipper> GetShipperByIdAsync(int shipperId);
        Task<IEnumerable<Shipper>> GetAllShippersAsync();
    }
}
