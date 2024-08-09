using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Concrete.Dapper
{
    public class ImageRepository : IImageRepository
    {
        private readonly IDbConnection _dbConnection;

        public ImageRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddImageAsync(Image image)
        {
            var query = @"INSERT INTO Images (ProductId, ImageUrl, CreatedDate, CreatedBy, IsActive) 
                          VALUES (@ProductId, @ImageUrl, @CreatedDate, @CreatedBy, @IsActive);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(query, image);
        }

        public async Task<int> UpdateImageAsync(Image image)
        {
            var query = @"UPDATE Images SET ImageUrl = @ImageUrl, UpdatedDate = @UpdatedDate, 
                          UpdatedBy = @UpdatedBy WHERE ImageId = @ImageId AND IsDeleted = 0";
            return await _dbConnection.ExecuteAsync(query, image);
        }

        public async Task<int> DeleteImageAsync(int imageId, int deletedBy)
        {
            var query = @"UPDATE Images SET DeletedDate = @DeletedDate, DeletedBy = @DeletedBy, 
                          IsDeleted = 1, IsActive = 0 WHERE ImageId = @ImageId";
            return await _dbConnection.ExecuteAsync(query, new { ImageId = imageId, DeletedDate = DateTime.UtcNow, DeletedBy = deletedBy });
        }

        public async Task<Image?> GetImageByIdAsync(int imageId)
        {
            var query = @"SELECT * FROM Images WHERE ImageId = @ImageId AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<Image>(query, new { ImageId = imageId });
        }

        public async Task<IEnumerable<Image>> GetAllImagesAsync()
        {
            var query = @"SELECT * FROM Images WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<Image>(query);
        }
    }
}
