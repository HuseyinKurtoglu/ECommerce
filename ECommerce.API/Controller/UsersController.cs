using Microsoft.AspNetCore.Mvc;
using ECommerce.Entities;
using System.Threading.Tasks;
using ECommerce.Business.Absract;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    // Constructor, IUserService bağımlılığını dependency injection yoluyla alır.
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // Belirli bir kullanıcıyı ID'sine göre getirir.
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        // Kullanıcı servisini kullanarak kullanıcıyı getirir.
        var result = _userService.GetUserById(id);
        // Sonuç başarılıysa kullanıcıyı döner, aksi takdirde hata mesajı döner.
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    // Yeni bir kullanıcı ekler.
    [HttpPost]
    public IActionResult AddUser([FromBody] User user)
    {
        // Kullanıcı servisini kullanarak yeni kullanıcıyı ekler.
        var result = _userService.AddUser(user);
        // Sonuç başarılıysa eklenen kullanıcıyı döner, aksi takdirde hata mesajı döner.
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }

    // Var olan bir kullanıcıyı günceller.
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] User user)
    {
        // Kullanıcı servisini kullanarak mevcut kullanıcıyı günceller.
        var result = _userService.UpdateUser(id, user);
        // Sonuç başarılıysa güncellenmiş kullanıcıyı döner, aksi takdirde hata mesajı döner.
        return result.Success ? Ok(result) : BadRequest(result.Message);
    }
}
