using Microsoft.AspNetCore.Mvc;
using ECommerce.Entities;

namespace ECommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var result = _userService.GetUserById(id);
            if (!result.Success)
            {
                return NotFound(result.Message);
            }
            return Ok(result.Data);
        }

        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var result = _userService.AddUser(user);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return CreatedAtAction(nameof(Get), new { id = result.Data.UserID }, result.Data);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var result = _userService.UpdateUser(id, user);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }
            return Ok(result.Data);
        }
    }
}
