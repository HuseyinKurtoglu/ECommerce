using System;
using ECommerce.DataAcces;
using ECommerce.Entities;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public ServiceResult<User> GetUserById(int userId)
    {
        try
        {
            var user = _userRepository.GetUserById(userId);
            if (user == null)
            {
                return new ServiceResult<User> { Success = false, Message = "User not found." };
            }
            return new ServiceResult<User> { Success = true, Data = user };
        }
        catch (Exception ex)
        {
            return new ServiceResult<User> { Success = false, Message = $"Error user by UserID: {ex.Message}" };
        }
    }

    public ServiceResult<User> AddUser(User user)
    {
        try
        {
            if (user == null)
            {
                return new ServiceResult<User> { Success = false, Message = "User object is null." };
            }

            _userRepository.AddUser(user);
            return new ServiceResult<User> { Success = true, Data = user };
        }
        catch (Exception ex)
        {
            return new ServiceResult<User> { Success = false, Message = $"Error adding user: {ex.Message}" };
        }
    }

    public ServiceResult<User> UpdateUser(int userId, User user)
    {
        try
        {
            var existingUser = _userRepository.GetUserById(userId);
            if (existingUser == null)
            {
                return new ServiceResult<User> { Success = false, Message = "User not found." };
            }

            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.UpdatedDate = DateTime.UtcNow;

            _userRepository.UpdateUser(existingUser);
            return new ServiceResult<User> { Success = true, Data = existingUser };
        }
        catch (Exception ex)
        {
            return new ServiceResult<User> { Success = false, Message = $"Error updating user: {ex.Message}" };
        }
    }
}
