using System.Collections.Generic;
using System.Threading.Tasks;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Absract
{
    public interface IProductReviewRepository
    {
        // Tüm ürün incelemelerini asenkron olarak getirir
        Task<IEnumerable<ProductReview>> GetAllProductReviewsAsync();

        // Belirli bir ürün incelemesini ID'ye göre asenkron olarak getirir
        Task<ProductReview> GetProductReviewByIdAsync(int reviewId);

        // Yeni bir ürün incelemesini asenkron olarak ekler ve eklenen incelemenin ID'sini döner
        Task<int> AddProductReviewAsync(ProductReview productReview);

        // Mevcut bir ürün incelemesini asenkron olarak günceller
        Task<int> UpdateProductReviewAsync(ProductReview productReview);

        // Bir ürün incelemesini asenkron olarak siler (yumuşak silme)
        Task<int> DeleteProductReviewAsync(int reviewId, int deletedBy);
    }
}

