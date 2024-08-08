using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Concrete
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IDbConnection _dbConnection;

        // Constructor, IDbConnection bağımlılığını alır
        public CategoryRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Tüm kategorileri asenkron olarak getirir
        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            // Silinmiş (IsDeleted = 1) olmayan kategorileri seçer
            var query = "SELECT * FROM Categories WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<Category>(query);
        }

        // Belirli bir kategoriyi ID'ye göre asenkron olarak getirir
        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            // Belirtilen ID'ye sahip ve silinmemiş kategoriyi seçer
            var query = "SELECT * FROM Categories WHERE CategoryId = @CategoryId AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<Category>(query, new { CategoryId = categoryId });
        }

        // Yeni bir kategoriyi asenkron olarak ekler
        public async Task<int> AddCategoryAsync(Category category)
        {
            // Kategoriyi ekler
            var query = "INSERT INTO Categories (CategoryName, Description, CreatedDate, CreatedBy, IsActive) VALUES (@CategoryName, @Description, @CreatedDate, @CreatedBy, @IsActive)";
            return await _dbConnection.ExecuteAsync(query, category);
        }

        // Var olan bir kategoriyi asenkron olarak günceller
        public async Task<int> UpdateCategoryAsync(Category category)
        {
            // Belirtilen ID'ye sahip kategoriyi günceller
            var query = "UPDATE Categories SET CategoryName = @CategoryName, Description = @Description, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, IsActive = @IsActive WHERE CategoryId = @CategoryId";
            return await _dbConnection.ExecuteAsync(query, category);
        }

        // Belirli bir kategoriyi asenkron olarak siler (IsDeleted bayrağını 1 yapar)
        public async Task<int> DeleteCategoryAsync(int categoryId, int deletedBy)
        {
            // Kategoriyi siler (IsDeleted bayrağını 1 yapar)
            var query = "UPDATE Categories SET IsDeleted = 1, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE CategoryId = @CategoryId";
            return await _dbConnection.ExecuteAsync(query, new { DeletedDate = DateTime.Now, DeletedBy = deletedBy, CategoryId = categoryId });
        }
    }
}
