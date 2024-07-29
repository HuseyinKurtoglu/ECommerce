using ECommerce.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            _userService.AddUser(user);
            return Ok();
        }

        [HttpGet]
        public IActionResult Get(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "HATA57 " + ex.Message);
            }
        }
        [HttpPut("{UserID}")]
        public IActionResult Update(int UserID, [FromBody] User user)
        {
            var findResult = _userService.GetUserById(UserID);
            if (findResult == null)
            {
                return NotFound();
            }

     
            _userService.UpdateUser(findResult);

            return Ok(findResult);
        }
    }

}
