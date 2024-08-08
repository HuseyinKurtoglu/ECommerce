using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Absract
{
    public interface ICategoryService
    {
        // Tüm kategorileri asenkron olarak getirir
        // Başarı durumu ve veri (kategoriler) döndürür
        Task<ServiceResult<IEnumerable<Category>>> GetAllCategoriesAsync();

        // Belirli bir kategori ID'sine göre kategoriyi asenkron olarak getirir
        // Başarı durumu ve veri (kategori) döndürür
        Task<ServiceResult<Category>> GetCategoryByIdAsync(int categoryId);

        // Yeni bir kategoriyi asenkron olarak ekler
        // Başarı durumu ve eklenen kategorinin ID'sini döndürür
        Task<ServiceResult<int>> AddCategoryAsync(Category category);

        // Var olan bir kategoriyi asenkron olarak günceller
        // Başarı durumu ve güncellenen satır sayısını döndürür
        Task<ServiceResult<int>> UpdateCategoryAsync(Category category);

        // Belirli bir kategori ID'sini asenkron olarak siler (IsDeleted bayrağını günceller)
        // Başarı durumu ve etkilenen satır sayısını döndürür
        Task<ServiceResult<int>> DeleteCategoryAsync(int categoryId, int deletedBy);
    }
}
