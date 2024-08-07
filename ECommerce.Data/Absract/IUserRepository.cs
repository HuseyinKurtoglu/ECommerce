using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    // Kullanıcıyı ekler.
    void AddUser(User user);

    // Belirli bir kullanıcıyı ID'sine göre getirir.
    User GetUserById(int id);

    // Mevcut bir kullanıcıyı günceller.
    void UpdateUser(User user);
}
