using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    // Ürünlerle ilgili veri erişim işlemlerini tanımlayan arayüz
    public interface IProductRepository
    {
        // Tüm ürünleri asenkron olarak getirir
        Task<IEnumerable<Product>> GetAllProductsAsync();

        // Belirli bir ürün ID'sine sahip ürünü asenkron olarak getirir
        Task<Product> GetProductByIdAsync(int productId);

        // Yeni bir ürünü asenkron olarak ekler ve eklenen ürünün ID'sini döner
        Task<int> AddProductAsync(Product product);

        // Var olan bir ürünü asenkron olarak günceller ve güncellenen satır sayısını döner
        Task<int> UpdateProductAsync(Product product);

        // Belirli bir ürün ID'sine sahip ürünü asenkron olarak siler ve silinen satır sayısını döner
        Task<int> DeleteProductAsync(int productId, int deletedBy);
    }
}

