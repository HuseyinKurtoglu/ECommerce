using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    void AddUser(User user);
    User GetUserById(int id);
    void UpdateUser(User user);
}
