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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            var sql = "SELECT * FROM Customers WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<Customer>(sql);
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            var sql = "SELECT * FROM Customers WHERE CustomerID = @CustomerID AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { CustomerID = customerId });
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            var sql = "INSERT INTO Customers (FirstName, LastName, Email, Phone, Address, City, Country, PostalCode, CreatedDate, CreatedBy, IsActive) VALUES (@FirstName, @LastName, @Email, @Phone, @Address, @City, @Country, @PostalCode, @CreatedDate, @CreatedBy, @IsActive); SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(sql, customer);
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            var sql = "UPDATE Customers SET FirstName = @FirstName, LastName = @LastName, Email = @Email, Phone = @Phone, Address = @Address, City = @City, Country = @Country, PostalCode = @PostalCode, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy, IsActive = @IsActive WHERE CustomerID = @CustomerID";
            return await _dbConnection.ExecuteAsync(sql, customer);
        }

        public async Task<int> DeleteCustomerAsync(int customerId, int deletedBy)
        {
            var sql = "UPDATE Customers SET IsDeleted = 1, DeletedDate = @DeletedDate, DeletedBy = @DeletedBy WHERE CustomerID = @CustomerID";
            return await _dbConnection.ExecuteAsync(sql, new { DeletedDate = DateTime.Now, DeletedBy = deletedBy, CustomerID = customerId });
        }
    }

}
