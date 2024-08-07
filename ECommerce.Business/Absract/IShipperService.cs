using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ECommerce.Business.Absract
{
 
    public interface IShipperService
    {
        // Bir shipper'ı asenkron olarak ekleyen metodu tanımlar
        Task<ServiceResult<int>> AddShipperAsync(Shipper shipper);

        // Var olan bir shipper'ı asenkron olarak güncelleyen metodu tanımlar
        Task<ServiceResult<int>> UpdateShipperAsync(Shipper shipper);

        // Belirli bir shipper'ı asenkron olarak silen metodu tanımlar
        Task<ServiceResult<int>> DeleteShipperAsync(int shipperId);

        // Belirli bir shipper'ı asenkron olarak ID ile getiren metodu tanımlar
        Task<ServiceResult<Shipper>> GetShipperByIdAsync(int shipperId);

        // Tüm shipper'ları asenkron olarak getiren metodu tanımlar
        Task<ServiceResult<IEnumerable<Shipper>>> GetAllShippersAsync();
    }
}
