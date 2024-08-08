using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    public interface ICategoryRepository
    {
        // Tüm kategorileri asenkron olarak getirir
        Task<IEnumerable<Category>> GetAllCategoriesAsync();

        // Belirli bir kategori ID'sine göre kategoriyi asenkron olarak getirir
        Task<Category> GetCategoryByIdAsync(int categoryId);

        // Yeni bir kategoriyi asenkron olarak ekler
        // Eklenen kategorinin ID'sini döndürür
        Task<int> AddCategoryAsync(Category category);

        // Var olan bir kategoriyi asenkron olarak günceller
        // Güncellenen satır sayısını döndürür
        Task<int> UpdateCategoryAsync(Category category);

        // Belirli bir kategori ID'sini asenkron olarak siler (IsDeleted bayrağını günceller)
        // Etkilenen satır sayısını döndürür
        Task<int> DeleteCategoryAsync(int categoryId, int deletedBy);
    }
}
