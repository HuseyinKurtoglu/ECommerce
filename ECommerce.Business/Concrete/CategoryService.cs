using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        // Constructor, ICategoryRepository bağımlılığını alır
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        // Tüm kategorileri asenkron olarak getirir
        public async Task<ServiceResult<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            try
            {
                // Kategorileri veri erişim katmanından alır
                var categories = await _categoryRepository.GetAllCategoriesAsync();
                // Başarı durumunda başarı mesajı ile döner
                return ServiceResult<IEnumerable<Category>>.SuccessResult(categories, "Kategoriler başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı ile döner
                return ServiceResult<IEnumerable<Category>>.FailureResult($"Kategoriler getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir kategoriyi ID'ye göre asenkron olarak getirir
        public async Task<ServiceResult<Category>> GetCategoryByIdAsync(int categoryId)
        {
            try
            {
                // Belirtilen ID'ye sahip kategoriyi veri erişim katmanından alır
                var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
                // Kategori bulunduysa başarı, bulunamadıysa hata mesajı ile döner
                return category != null
                    ? ServiceResult<Category>.SuccessResult(category, "Kategori başarıyla getirildi.", HttpStatusCode.OK)
                    : ServiceResult<Category>.FailureResult("Kategori bulunamadı.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı ile döner
                return ServiceResult<Category>.FailureResult($"Kategori getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Yeni bir kategoriyi asenkron olarak ekler
        public async Task<ServiceResult<int>> AddCategoryAsync(Category category)
        {
            try
            {
                // Yeni kategoriyi veri erişim katmanına ekler
                var result = await _categoryRepository.AddCategoryAsync(category);
                // Başarı durumunda kategori ID'si ile döner
                return ServiceResult<int>.SuccessResult(result, "Kategori başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı ile döner
                return ServiceResult<int>.FailureResult($"Kategori eklenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Var olan bir kategoriyi asenkron olarak günceller
        public async Task<ServiceResult<int>> UpdateCategoryAsync(Category category)
        {
            try
            {
                // Kategoriyi veri erişim katmanında günceller
                var result = await _categoryRepository.UpdateCategoryAsync(category);
                // Güncelleme başarılıysa başarı, aksi takdirde hata mesajı ile döner
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Kategori başarıyla güncellendi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Kategori güncellenemedi.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı ile döner
                return ServiceResult<int>.FailureResult($"Kategori güncellenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir kategoriyi asenkron olarak siler
        public async Task<ServiceResult<int>> DeleteCategoryAsync(int categoryId, int deletedBy)
        {
            try
            {
                // Kategoriyi veri erişim katmanında siler
                var result = await _categoryRepository.DeleteCategoryAsync(categoryId, deletedBy);
                // Silme başarılıysa başarı, aksi takdirde hata mesajı ile döner
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Kategori başarıyla silindi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Kategori silinemedi.", HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajı ile döner
                return ServiceResult<int>.FailureResult($"Kategori silinirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
