using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using ECommerce.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
namespace ECommerce.DataAcces.Concrete
{
    // IShipperRepository arayüzünü implement eden ShipperRepository sınıfı
    public class ShipperRepository : IShipperRepository
    {
        // Bağlantı ayarlarını almak için IConfiguration nesnesi
        private readonly IConfiguration _configuration;

        // ShipperRepository sınıfının yapıcısı, IConfiguration nesnesini alır
        public ShipperRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // SQL veritabanı bağlantısı oluşturur
        private IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        // Yeni bir nakliyeci ekler
        public async Task<int> AddShipperAsync(Shipper shipper)
        {
            // Ekleme sorgusu, eklenen nakliyecinin ID'sini döner
            var sql = @"INSERT INTO Shippers (CompanyName, Phone, CreatedDate, CreatedBy, UpdatedDate, UpdatedBy, DeletedDate, DeletedBy, IsDeleted, IsActive) 
                        VALUES (@CompanyName, @Phone, @CreatedDate, @CreatedBy, @UpdatedDate, @UpdatedBy, @DeletedDate, @DeletedBy, @IsDeleted, @IsActive);
                        SELECT CAST(SCOPE_IDENTITY() as int);";

            // Bağlantıyı oluşturur ve sorguyu çalıştırır
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteScalarAsync<int>(sql, shipper);
            }
        }

        // Var olan bir nakliyeciyi günceller
        public async Task<int> UpdateShipperAsync(Shipper shipper)
        {
            // Güncelleme sorgusu, etkilenen satır sayısını döner
            var sql = @"UPDATE Shippers 
                        SET CompanyName = @CompanyName, 
                            Phone = @Phone, 
                            UpdatedDate = @UpdatedDate, 
                            UpdatedBy = @UpdatedBy,
                            IsDeleted = @IsDeleted,
                            IsActive = @IsActive
                        WHERE ShipperID = @ShipperID";

            // Bağlantıyı oluşturur ve sorguyu çalıştırır
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, shipper);
            }
        }

        // Belirli bir nakliyeciyi siler
        public async Task<int> DeleteShipperAsync(int shipperId)
        {
            // Silme sorgusu, etkilenen satır sayısını döner
            var sql = @"DELETE FROM Shippers WHERE ShipperID = @ShipperID";

            // Bağlantıyı oluşturur ve sorguyu çalıştırır
            using (var connection = CreateConnection())
            {
                return await connection.ExecuteAsync(sql, new { ShipperID = shipperId });
            }
        }

        // Belirli bir nakliyeci ID'sine sahip nakliyeciyi getirir
        public async Task<Shipper> GetShipperByIdAsync(int shipperId)
        {
            // Seçim sorgusu, nakliyeciyi döner
            var sql = @"SELECT * FROM Shippers WHERE ShipperID = @ShipperID";

            // Bağlantıyı oluşturur ve sorguyu çalıştırır
            using (var connection = CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<Shipper>(sql, new { ShipperID = shipperId });
            }
        }

        // Tüm nakliyecileri getirir
        public async Task<IEnumerable<Shipper>> GetAllShippersAsync()
        {
            // Tüm nakliyecileri getiren sorgu
            var sql = @"SELECT * FROM Shippers";

            // Bağlantıyı oluşturur ve sorguyu çalıştırır
            using (var connection = CreateConnection())
            {
                return await connection.QueryAsync<Shipper>(sql);
            }
        }
    }
}
