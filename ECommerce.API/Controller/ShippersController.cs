using ECommerce.Business.Absract;
using ECommerce.DataAcces.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace ECommerce.API.Controller
{
    // API yolunu "api/shippers" olarak ayarlayan ve API Controller olarak işaretleyen attribute
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperService _shipperService; // IShipperService türünde bir iş servisi nesnesi

        // Constructor'da dependency injection yoluyla IShipperService nesnesini alır
        public ShippersController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        // Yeni bir shipper ekler
        [HttpPost]
        public async Task<IActionResult> AddShipper([FromBody] Shipper shipper)
        {
            // Servis üzerinden shipper ekleme işlemi gerçekleştirilir
            var result = await _shipperService.AddShipperAsync(shipper);
            return StatusCode((int)result.StatusCode, result);
        }

        // Var olan bir shipper'ı günceller
        [HttpPut]
        public async Task<IActionResult> UpdateShipper([FromBody] Shipper shipper)
        {
            // Servis üzerinden shipper güncelleme işlemi gerçekleştirilir
            var result = await _shipperService.UpdateShipperAsync(shipper);
            return StatusCode((int)result.StatusCode, result);
        }

        // Bir shipper'ı ID ile siler
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            // Servis üzerinden shipper silme işlemi gerçekleştirilir
            var result = await _shipperService.DeleteShipperAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        // ID ile bir shipper'ı getirir
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipperById(int id)
        {
            // Servis üzerinden shipper getirme işlemi gerçekleştirilir
            var result = await _shipperService.GetShipperByIdAsync(id);
            return StatusCode((int)result.StatusCode, result);
        }

        // Tüm shipper'ları getirir
        [HttpGet]
        public async Task<IActionResult> GetAllShippers()
        {
            // Servis üzerinden tüm shipper'ları getirme işlemi gerçekleştirilir
            var result = await _shipperService.GetAllShippersAsync();
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
