using ECommerce.Business;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    // Kullanıcıyı ekler ve işlem sonucunu döner.
    ServiceResult<User> AddUser(User user);

    // Belirli bir kullanıcıyı ID'sine göre getirir ve işlem sonucunu döner.
    ServiceResult<User> GetUserById(int id);

    // Mevcut bir kullanıcıyı günceller ve işlem sonucunu döner.
    ServiceResult<User> UpdateUser(int id, User user);
}
