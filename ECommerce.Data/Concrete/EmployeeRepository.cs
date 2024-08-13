using Dapper;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Concrete.Dapper
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnection _dbConnection;

        public EmployeeRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddEmployeeAsync(Employee employee)
        {
            var query = @"INSERT INTO Employees (FirstName, LastName, Email, Phone, HireDate, JobTitle, CreatedDate, CreatedBy, IsActive) 
                          VALUES (@FirstName, @LastName, @Email, @Phone, @HireDate, @JobTitle, @CreatedDate, @CreatedBy, @IsActive);
                          SELECT CAST(SCOPE_IDENTITY() as int)";
            return await _dbConnection.ExecuteScalarAsync<int>(query, employee);
        }

        public async Task<int> UpdateEmployeeAsync(Employee employee)
        {
            var query = @"UPDATE Employees SET FirstName = @FirstName, LastName = @LastName, Email = @Email, 
                          Phone = @Phone, HireDate = @HireDate, JobTitle = @JobTitle, 
                          UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy 
                          WHERE EmployeeId = @EmployeeId AND IsDeleted = 0";
            return await _dbConnection.ExecuteAsync(query, employee);
        }

        public async Task<int> DeleteEmployeeAsync(int employeeId, int deletedBy)
        {
            var query = @"UPDATE Employees SET DeletedDate = @DeletedDate, DeletedBy = @DeletedBy, 
                          IsDeleted = 1, IsActive = 0 WHERE EmployeeId = @EmployeeId";
            return await _dbConnection.ExecuteAsync(query, new { EmployeeId = employeeId, DeletedDate = DateTime.UtcNow, DeletedBy = deletedBy });
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {
            var query = @"SELECT * FROM Employees WHERE EmployeeId = @EmployeeId AND IsDeleted = 0";
            return await _dbConnection.QueryFirstOrDefaultAsync<Employee>(query, new { EmployeeId = employeeId });
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            var query = @"SELECT * FROM Employees WHERE IsDeleted = 0";
            return await _dbConnection.QueryAsync<Employee>(query);
        }
    }
}
