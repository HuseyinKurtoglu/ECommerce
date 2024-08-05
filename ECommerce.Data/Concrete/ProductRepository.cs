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
    public class ProductRepository : IProductRepository
    {
        private readonly IDbConnection _dbConnection;

        public ProductRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            var sql = "SELECT * FROM Products WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<Product>(sql);
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            var sql = "SELECT * FROM Products WHERE ProductID = @ProductID AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { ProductID = productId });
        }

        public async Task<int> AddProductAsync(Product product)
        {
            var sql = "INSERT INTO Products (ProductName, Description, Price, StockQuantity, CategoryID, CreatedDate, CreatedBy, IsActive) VALUES (@ProductName, @Description, @Price, @StockQuantity, @CategoryID, @CreatedDate, @CreatedBy, @IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, product);
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            var sql = "UPDATE Products SET ProductName = @ProductName, Description = @Description, Price = @Price, StockQuantity = @StockQuantity, CategoryID = @CategoryID, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, IsActive = @IsActive WHERE ProductID = @ProductID";
            return await _dbConnection.ExecuteAsync(sql, product);
        }

        public async Task<int> DeleteProductAsync(int productId, int deletedBy)
        {
            var sql = "UPDATE Products SET IsDeleted = 1, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE ProductID = @ProductID";
            return await _dbConnection.ExecuteAsync(sql, new { DeletedDate = DateTime.Now, DeletedBy = deletedBy, ProductID = productId });
        }
    }

}
