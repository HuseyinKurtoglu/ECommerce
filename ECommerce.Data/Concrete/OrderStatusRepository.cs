using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

namespace ECommerce.DataAcces.Concrete
{
    public class OrderStatusRepository : IOrderStatusRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrderStatusRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<OrderStatus>> GetAllOrderStatusesAsync()
        {
            var sql = "SELECT * FROM OrderStatus WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<OrderStatus>(sql);
        }

        public async Task<OrderStatus> GetOrderStatusByIdAsync(int statusId)
        {
            var sql = "SELECT * FROM OrderStatus WHERE StatusID = @StatusID AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<OrderStatus>(sql, new { StatusID = statusId });
        }

        public async Task<int> AddOrderStatusAsync(OrderStatus orderStatus)
        {
            var sql = "INSERT INTO OrderStatus (StatusDescription, CreatedDate, CreatedBy, IsActive) VALUES (@StatusDescription, @CreatedDate, @CreatedBy, @IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, orderStatus);
        }

        public async Task<int> UpdateOrderStatusAsync(OrderStatus orderStatus)
        {
            var sql = "UPDATE OrderStatus SET StatusDescription = @StatusDescription, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, IsActive = @IsActive WHERE StatusID = @StatusID";
            return await _dbConnection.ExecuteAsync(sql, orderStatus);
        }

        public async Task<int> DeleteOrderStatusAsync(int statusId, int deletedBy)
        {
            var sql = "UPDATE OrderStatus SET IsDeleted = 1, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE StatusID = @StatusID";
            return await _dbConnection.ExecuteAsync(sql, new { DeletedDate = DateTime.Now, DeletedBy = deletedBy, StatusID = statusId });
        }
    }
}
