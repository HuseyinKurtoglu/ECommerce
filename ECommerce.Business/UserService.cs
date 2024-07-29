
using ECommerce.Data;
using ECommerce.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public void AddUser(User user)
    {
        _userRepository.AddUser(user);
    }
    public User GetUserById(int UserID)
    {
     return _userRepository.GetUserById(UserID);
    }

    public void UpdateUser(User user)  
    {
        _userRepository.UpdateUser(user);
    }
}
