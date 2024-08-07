using ECommerce.DataAcces.Entity;
using ECommerce.Entities;

public class UserRepository : IUserRepository
{
    private readonly ECommerceDbContext _context;

    // Constructor, ECommerceDbContext bağımlılığını dependency injection yoluyla alır.
    public UserRepository(ECommerceDbContext context)
    {
        _context = context;
    }

    // Belirli bir kullanıcıyı ID'sine göre getirir.
    public User GetUserById(int userId)
    {
        return _context.Users.Find(userId);
    }

    // Yeni bir kullanıcıyı veritabanına ekler.
    public void AddUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    // Mevcut bir kullanıcıyı günceller.
    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
