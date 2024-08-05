using Dapper;
using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECommerce.DataAccess
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly IConfiguration _configuration;

        public ShipperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async Task<int> AddShipperAsync(Shipper shipper)
        {
            var sql = @"INSERT INTO Shippers (CompanyName, Phone, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, IsDeleted, IsActive) 
                        VALUES (@CompanyName, @Phone, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy, @DeletedDate, @DeletedBy, @IsDeleted, @IsActive);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, shipper);
            }
        }

        public async Task<int> UpdateShipperAsync(Shipper shipper)
        {
            var sql = @"UPDATE Shippers 
                        SET CompanyName = @CompanyName, 
                            Phone = @Phone, 
                            UpdatedDate = @UpdatedDate, 
                            UpdatedBy = @UpdatedBy,
                            IsDeleted = @IsDeleted,
                            IsActive = @IsActive
                        WHERE ShipperID = @ShipperID";

            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, shipper);
            }
        }

        public async Task<int> DeleteShipperAsync(int shipperId)
        {
            var sql = @"DELETE FROM Shippers WHERE ShipperID = @ShipperID";

            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, new { ShipperID = shipperId });
            }
        }

        public async Task<Shipper> GetShipperByIdAsync(int shipperId)
        {
            var sql = @"SELECT * FROM Shippers WHERE ShipperID = @ShipperID";

            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Shipper>(sql, new { ShipperID = shipperId });
            }
        }

        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            var sql = @"SELECT * FROM Shippers";

            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Shipper>(sql);
            }
        }
    }
}
