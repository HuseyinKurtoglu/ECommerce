using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Absract
{
    public interface IProductReviewService
    {
        Task<ServiceResult<IEnumerable<ProductReview>>> GetAllProductReviewsAsync();
        Task<ServiceResult<ProductReview>> GetProductReviewByIdAsync(int reviewId);
        Task<ServiceResult<int>> AddProductReviewAsync(ProductReview productReview);
        Task<ServiceResult<int>> UpdateProductReviewAsync(ProductReview productReview);
        Task<ServiceResult<int>> DeleteProductReviewAsync(int reviewId, int deletedBy);
    }
}
