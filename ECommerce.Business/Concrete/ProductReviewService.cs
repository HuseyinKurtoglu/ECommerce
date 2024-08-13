using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

namespace ECommerce.Business.Concrete
{
    public class ProductReviewService : IProductReviewService
    {
        private readonly IProductReviewRepository _productReviewRepository;

        // Constructor, bağımlılığı enjeksiyon yoluyla alır
        public ProductReviewService(IProductReviewRepository productReviewRepository)
        {
            _productReviewRepository = productReviewRepository;
        }

        // Tüm ürün yorumlarını asenkron olarak getirir
        public async Task<ServiceResult<IEnumerable<ProductReview>>> GetAllProductReviewsAsync()
        {
            try
            {
                // Ürün yorumlarını veri erişim katmanından alır
                var result = await _productReviewRepository.GetAllProductReviewsAsync();
                // Başarıyla getirildiğinde ServiceResult ile döner
                return ServiceResult<IEnumerable<ProductReview>>.SuccessResult(result, "Ürün yorumları başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                return ServiceResult<IEnumerable<ProductReview>>.FailureResult($"Ürün yorumları getirilirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir ürün yorumunu asenkron olarak getirir
        public async Task<ServiceResult<ProductReview>> GetProductReviewByIdAsync(int reviewId)
        {
            try
            {
                // Belirtilen ID'ye sahip ürün yorumunu veri erişim katmanından alır
                var result = await _productReviewRepository.GetProductReviewByIdAsync(reviewId);
                if (result == null)
                    // Ürün yorumu bulunamadığında hata mesajı döner
                    return ServiceResult<ProductReview>.FailureResult("Ürün yorumu bulunamadı.", HttpStatusCode.NotFound);

                // Ürün yorumu başarıyla getirildiğinde ServiceResult ile döner
                return ServiceResult<ProductReview>.SuccessResult(result, "Ürün yorumu başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                return ServiceResult<ProductReview>.FailureResult($"Ürün yorumu getirilirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Yeni bir ürün yorumu asenkron olarak ekler
        public async Task<ServiceResult<int>> AddProductReviewAsync(ProductReview productReview)
        {
            try
            {
                // Yeni ürünü veri erişim katmanına ekler ve eklenen yorumun ID'sini döner
                var result = await _productReviewRepository.AddProductReviewAsync(productReview);
                return ServiceResult<int>.SuccessResult(result, "Ürün yorumu başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                return ServiceResult<int>.FailureResult($"Ürün yorumu eklenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Var olan bir ürün yorumunu asenkron olarak günceller
        public async Task<ServiceResult<int>> UpdateProductReviewAsync(ProductReview productReview)
        {
            try
            {
                // Ürün yorumunu veri erişim katmanında günceller
                var result = await _productReviewRepository.UpdateProductReviewAsync(productReview);
                return ServiceResult<int>.SuccessResult(result, "Ürün yorumu başarıyla güncellendi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                return ServiceResult<int>.FailureResult($"Ürün yorumu güncellenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir ürün yorumunu asenkron olarak siler
        public async Task<ServiceResult<int>> DeleteProductReviewAsync(int reviewId, int deletedBy)
        {
            try
            {
                // Ürün yorumunu veri erişim katmanında siler
                var result = await _productReviewRepository.DeleteProductReviewAsync(reviewId, deletedBy);
                return ServiceResult<int>.SuccessResult(result, "Ürün yorumu başarıyla silindi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda ServiceResult ile hata mesajı döner
                return ServiceResult<int>.FailureResult($"Ürün yorumu silinirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
