using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository;

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository;
        }

        public async Task<int> AddShipperAsync(Shipper shipper)
        {
            try
            {
                return await _shipperRepository.AddShipperAsync(shipper);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding shipper", ex);
            }
        }

        public async Task<int> UpdateShipperAsync(Shipper shipper)
        {
            try
            {
                return await _shipperRepository.UpdateShipperAsync(shipper);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating shipper", ex);
            }
        }

        public async Task<int> DeleteShipperAsync(int shipperId)
        {
            try
            {
                return await _shipperRepository.DeleteShipperAsync(shipperId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting shipper", ex);
            }
        }

        public async Task<Shipper> GetShipperByIdAsync(int shipperId)
        {
            try
            {
                return await _shipperRepository.GetShipperByIdAsync(shipperId);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching shipper by id", ex);
            }
        }

        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            try
            {
                return await _shipperRepository.GetAllShippersAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching all shippers", ex);
            }
        }
    }
}
