using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Absract
{
    public interface IEmployeeService
    {
        Task<ServiceResult<int>> AddEmployeeAsync(Employee employee);
        Task<ServiceResult<int>> UpdateEmployeeAsync(Employee employee);
        Task<ServiceResult<int>> DeleteEmployeeAsync(int employeeId, int deletedBy);
        Task<ServiceResult<Employee?>> GetEmployeeByIdAsync(int employeeId);
        Task<ServiceResult<IEnumerable<Employee>>> GetAllEmployeesAsync();
    }
}
