using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    public interface IEmployeeRepository
    {
        Task<int> AddEmployeeAsync(Employee employee);
        Task<int> UpdateEmployeeAsync(Employee employee);
        Task<int> DeleteEmployeeAsync(int employeeId, int deletedBy);
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    }
}
