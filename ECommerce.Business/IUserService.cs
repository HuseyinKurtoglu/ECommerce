using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IUserService
{
    ServiceResult<User> AddUser(User user);
    ServiceResult<User> GetUserById(int id);
    ServiceResult<User> UpdateUser(int id, User user);
}
