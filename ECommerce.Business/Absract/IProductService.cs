using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Absract
{
    public interface IProductService
    {
        // Tüm ürünleri getirir
        Task<ServiceResult<IEnumerable<Product>>> GetAllProductsAsync();

        // ID ile ürün getirir
        Task<ServiceResult<Product>> GetProductByIdAsync(int productId);

        // Yeni ürün ekler
        Task<ServiceResult<int>> AddProductAsync(Product product);

        // Ürün günceller
        Task<ServiceResult<int>> UpdateProductAsync(Product product);

        // Ürün siler
        Task<ServiceResult<int>> DeleteProductAsync(int productId, int deletedBy);
    }
}
