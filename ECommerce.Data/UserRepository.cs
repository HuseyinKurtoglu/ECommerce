using ECommerce.Data;
using ECommerce.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserRepository : IUserRepository
{
    private readonly ECommerceDbContext _context;

    public UserRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();

    }
    public User GetUserById(int UserID)
    {

        return _context.Users.Find(UserID);

    }
    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
