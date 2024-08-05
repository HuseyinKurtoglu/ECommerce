using ECommerce.Business;
using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShippersController : ControllerBase
    {
        private readonly IShipperService _shipperService;

        public ShippersController(IShipperService shipperService)
        {
            _shipperService = shipperService;
        }

        [HttpPost]
        public async Task<IActionResult> AddShipper([FromBody] Shipper shipper)
        {
            if (shipper == null)
            {
                return BadRequest("Shipper is null.");
            }

            var result = await _shipperService.AddShipperAsync(shipper);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateShipper([FromBody] Shipper shipper)
        {
            if (shipper == null)
            {
                return BadRequest("Shipper is null.");
            }

            var result = await _shipperService.UpdateShipperAsync(shipper);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShipper(int id)
        {
            var result = await _shipperService.DeleteShipperAsync(id);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShipperById(int id)
        {
            var shipper = await _shipperService.GetShipperByIdAsync(id);
            if (shipper == null)
            {
                return NotFound();
            }
            return Ok(shipper);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllShippers()
        {
            var shippers = await _shipperService.GetAllShippersAsync();
            return Ok(shippers);
        }
    }
}
