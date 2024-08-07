using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Concrete
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _dbConnection;

        // Constructor, IDbConnection bağımlılığını dependency injection yoluyla alır.
        public OrderRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Belirli bir siparişi ID'sine göre asenkron olarak getirir.
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            // Sipariş bilgilerini getirir.
            var sql = "SELECT * FROM Orders WHERE OrderID = @OrderId AND IsDeleted = 0";
            var order = await _dbConnection.QueryFirstOrDefaultAsync<Order>(sql, new { OrderId = orderId });

            if (order != null)
            {
                // Sipariş detaylarını getirir.
                var orderDetailsSql = "SELECT * FROM OrderDetails WHERE OrderID = @OrderId AND IsDeleted = 0";
                order.OrderDetails = (await _dbConnection.QueryAsync<OrderDetail>(orderDetailsSql, new { OrderId = orderId })).ToList();
            }

            return order;
        }

        // Tüm siparişleri asenkron olarak getirir.
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var sql = "SELECT * FROM Orders WHERE IsDeleted = 0";
            var orders = await _dbConnection.QueryAsync<Order>(sql);
            return orders;
        }

        // Yeni bir siparişi asenkron olarak ekler.
        public async Task AddOrderAsync(Order order)
        {
            var sql = @"
            INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, ShipperID, StatusID, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, IsDeleted, IsActive)
            VALUES (@CustomerID, @OrderDate, @TotalAmount, @ShipperID, @StatusID, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy, @DeletedDate, @DeletedBy, @IsDeleted, @IsActive);
            SELECT CAST(SCOPE_IDENTITY() as int)";

            // Yeni siparişin ID'sini döner.
            var orderId = await _dbConnection.ExecuteScalarAsync<int>(sql, order);
            order.OrderId = orderId;

            // Sipariş detaylarını ekler.
            foreach (var detail in order.OrderDetails)
            {
                detail.OrderId = orderId;
                await AddOrderDetailAsync(detail);
            }
        }

        // Var olan bir siparişi asenkron olarak günceller.
        public async Task UpdateOrderAsync(Order order)
        {
            var sql = @"
            UPDATE Orders
            SET CustomerID = @CustomerID, OrderDate = @OrderDate, TotalAmount = @TotalAmount, ShipperID = @ShipperID, StatusID = @StatusID, 
                CreatedDate = @CreatedDate, CreatedBy = @CreatedBy, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, DeletedDate = @DeletedDate, 
                DeletedBy = @DeletedBy, IsDeleted = @IsDeleted, IsActive = @IsActive
            WHERE OrderID = @OrderID";

            await _dbConnection.ExecuteAsync(sql, order);

            // Sipariş detaylarını günceller.
            foreach (var detail in order.OrderDetails)
            {
                await UpdateOrderDetailAsync(detail);
            }
        }

        // Belirli bir siparişi ID'sine göre asenkron olarak siler (IsDeleted bayrağını set eder).
        public async Task DeleteOrderAsync(int orderId)
        {
            var sql = "UPDATE Orders SET IsDeleted = 1 WHERE OrderID = @OrderId";
            await _dbConnection.ExecuteAsync(sql, new { OrderId = orderId });
        }

        // Sipariş detayını asenkron olarak ekler.
        private async Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            var sql = @"
            INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, IsDeleted, IsActive)
            VALUES (@OrderID, @ProductID, @Quantity, @UnitPrice, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy, @DeletedDate, @DeletedBy, @IsDeleted, @IsActive)";

            await _dbConnection.ExecuteAsync(sql, orderDetail);
        }

        // Sipariş detayını asenkron olarak günceller.
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
