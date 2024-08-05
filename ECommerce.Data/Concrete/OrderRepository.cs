using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrderRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            var sql = "SELECT * FROM Orders WHERE OrderID = @OrderId AND IsDeleted = 0";
            var order = await _dbConnection.QueryFirstOrDefaultAsync<Order>(sql, new { OrderId = orderId });

            if (order != null)
            {
                var orderDetailsSql = "SELECT * FROM OrderDetails WHERE OrderID = @OrderId AND IsDeleted = 0";
                order.OrderDetails = (await _dbConnection.QueryAsync<OrderDetail>(orderDetailsSql, new { OrderId = orderId })).ToList();
            }

            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var sql = "SELECT * FROM Orders WHERE IsDeleted = 0";
            var orders = await _dbConnection.QueryAsync<Order>(sql);
            return orders;
        }

        public async Task AddOrderAsync(Order order)
        {
            var sql = @"
            INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, ShipperID, StatusID, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, IsDeleted, IsActive)
            VALUES (@CustomerID, @OrderDate, @TotalAmount, @ShipperID, @StatusID, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy, @DeletedDate, @DeletedBy, @IsDeleted, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int)";

            var orderId = await _dbConnection.ExecuteScalarAsync<int>(sql, order);
            order.OrderId = orderId;

            foreach (var detail in order.OrderDetails)
            {
                detail.OrderId = orderId;
                await AddOrderDetailAsync(detail);
            }
        }

        public async Task UpdateOrderAsync(Order order)
        {
            var sql = @"
            UPDATE Orders
            SET CustomerID = @CustomerID, OrderDate = @OrderDate, TotalAmount = @TotalAmount, ShipperID = @ShipperID, StatusID = @StatusID, 
                CreatedDate = @CreatedDate, CreatedBy = @CreatedBy, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, DeletedDate = @DeletedDate, 
                DeletedBy = @DeletedBy, IsDeleted = @IsDeleted, IsActive = @IsActive
            WHERE OrderID = @OrderID";

            await _dbConnection.ExecuteAsync(sql, order);

            foreach (var detail in order.OrderDetails)
            {
                await UpdateOrderDetailAsync(detail);
            }
        }

        public async Task DeleteOrderAsync(int orderId)
        {
            var sql = "UPDATE Orders SET IsDeleted = 1 WHERE OrderID = @OrderId";
            await _dbConnection.ExecuteAsync(sql, new { OrderId = orderId });
        }

        private async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            var sql = @"
            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, IsDeleted, IsActive)
            VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy, @DeletedDate, @DeletedBy, @IsDeleted, @IsActive)";

            await _dbConnection.ExecuteAsync(sql, orderDetail);
        }

        private async Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            var sql = @"
            UPDATE OrderDetails
            SET ProductID = @ProductID, Quantity = @Quantity, UnitPrice = @UnitPrice, CreatedDate = @CreatedDate, CreatedBy = @CreatedBy, 
                UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy, IsDeleted = @IsDeleted, IsActive = @IsActive
            WHERE OrderDetailID = @OrderDetailID";

            await _dbConnection.ExecuteAsync(sql, orderDetail);
        }
    }

}
