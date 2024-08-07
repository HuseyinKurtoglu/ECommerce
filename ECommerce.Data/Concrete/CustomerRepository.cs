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
    // Müşteri veritabanı işlemlerini gerçekleştiren sınıf
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        // Constructor, veritabanı bağlantısını alır ve sınıfın içindeki _dbConnection değişkenine atar
        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        // Tüm müşterileri asenkron olarak getirir
        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            // Aktif ve silinmemiş müşterileri seçmek için SQL sorgusu
            var sql = "SELECT * FROM Customers WHERE IsDeleted = 0";
            // Sorguyu çalıştırır ve müşteri listesini döner
            return await _dbConnection.QueryAsync<Customer>(sql);
        }

        // Belirli bir müşteri ID'sine sahip müşteri bilgisini asenkron olarak getirir
        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            // Müşteri ID'sine göre ve silinmemiş olan müşteriyi seçmek için SQL sorgusu
            var sql = "SELECT * FROM Customers WHERE CustomerID = @CustomerID AND IsDeleted = 0";
            // Sorguyu çalıştırır ve müşteri nesnesini döner; bulunamazsa null döner
            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { CustomerID = customerId });
        }

        // Yeni bir müşteri ekler ve eklenen müşterinin ID'sini döner
        public async Task<int> AddCustomerAsync(Customer customer)
        {
            // Yeni müşteri eklemek için SQL sorgusu
            // SCOPE_IDENTITY() ile eklenen müşteri için oluşturulan ID'yi döner
            var sql = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address, City, Country, PostalCode, CreatedDate, CreatedBy, IsActive) VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @City, @Country, @PostalCode, @CreatedDate, @CreatedBy, @IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
            // Sorguyu çalıştırır ve yeni müşteri ID'sini döner
            return await _dbConnection.ExecuteScalarAsync<int>(sql, customer);
        }

        // Var olan bir müşteriyi günceller ve güncellenen satır sayısını döner
        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            // Müşteri bilgilerini güncellemek için SQL sorgusu
            var sql = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Address = @Address, City = @City, Country = @Country, PostalCode = @PostalCode, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, IsActive = @IsActive WHERE CustomerID = @CustomerID";
            // Sorguyu çalıştırır ve güncellenen satır sayısını döner
            return await _dbConnection.ExecuteAsync(sql, customer);
        }

        // Belirli bir müşteri ID'sine sahip müşteriyi siler (mantıksal silme)
        public async Task<int> DeleteCustomerAsync(int customerId, int deletedBy)
        {
            // Müşteriyi mantıksal olarak silmek için SQL sorgusu
            var sql = "UPDATE Customers SET IsDeleted = 1, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE CustomerID = @CustomerID";
            // Sorguyu çalıştırır ve silinen satır sayısını döner
            return await _dbConnection.ExecuteAsync(sql, new { DeletedDate = DateTime.Now, DeletedBy = deletedBy, CustomerID = customerId });
        }
    }
}
