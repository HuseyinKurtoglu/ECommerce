using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.DataAcces.Models;

namespace ECommerce.Business.Absract
{
    public interface IShipperService
    {
        Task<int> AddShipperAsync(Shipper shipper);
        Task<int> UpdateShipperAsync(Shipper shipper);
        Task<int> DeleteShipperAsync(int shipperId);
        Task<Shipper> GetShipperByIdAsync(int shipperId);
        Task<IEnumerable<Shipper>> GetAllShippersAsync();
    }
}
