using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
namespace ECommerce.DataAcces.Concrete
{
    // Ürünlerle ilgili veri erişim işlemlerini gerçekleştiren sınıf
    public class ProductRepository : IProductRepository
    {
        // Veritabanı bağlantısı
        private readonly IDbConnection _dbConnection;

        // Constructor: Veritabanı bağlantısını alır
        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Tüm ürünleri getirir
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            // SQL sorgusu: Silinmemiş tüm ürünleri seçer
            var sql = "SELECT * FROM Products WHERE IsDeleted = 0";
            // Asenkron olarak ürünleri getirir
            return await _dbConnection.QueryAsync<Product>(sql);
        }

        // Belirli bir ürün ID'sine sahip ürünü getirir
        public async Task<Product> GetProductByIdAsync(int productId)
        {
            // SQL sorgusu: Verilen ID'ye sahip ve silinmemiş ürünü seçer
            var sql = "SELECT * FROM Products WHERE ProductID = @ProductID AND IsDeleted = 0";
            // Asenkron olarak ürün getirir, bulunamazsa null döner
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { ProductID = productId });
        }

        // Yeni bir ürün ekler
        public async Task<int> AddProductAsync(Product product)
        {
            // SQL sorgusu: Ürünü ekler ve eklenen ürünün ID'sini döner
            var sql = "INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryID, CreatedDate, CreatedBy, IsActive) VALUES (@ProductName, @Description, @Price, @StockQuantity, @CategoryID, @CreatedDate, @CreatedBy, @IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
            // Asenkron olarak ürünü ekler ve eklenen ürünün ID'sini döner
            return await _dbConnection.ExecuteScalarAsync<int>(sql, product);
        }

        // Var olan bir ürünü günceller
        public async Task<int> UpdateProductAsync(Product product)
        {
            // SQL sorgusu: Ürünün bilgilerini günceller
            var sql = "UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, CategoryID = @CategoryID, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, IsActive = @IsActive WHERE ProductID = @ProductID";
            // Asenkron olarak ürünü günceller ve güncellenen satır sayısını döner
            return await _dbConnection.ExecuteAsync(sql, product);
        }

        // Belirli bir ürün ID'sine sahip ürünü siler
        public async Task<int> DeleteProductAsync(int productId, int deletedBy)
        {
            // SQL sorgusu: Ürünü mantıksal olarak siler, yani IsDeleted = 1 olarak günceller
            var sql = "UPDATE Products SET IsDeleted = 1, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE ProductID = @ProductID";
            // Asenkron olarak ürünü siler ve etkilenen satır sayısını döner
            return await _dbConnection.ExecuteAsync(sql, new { DeletedDate = DateTime.Now, DeletedBy = deletedBy, ProductID = productId });
        }
    }
}
