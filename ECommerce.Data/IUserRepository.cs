using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserRepository
{
    void AddUser(User user);
    User GetUserById(int id);
    void UpdateUser(User user);
}
