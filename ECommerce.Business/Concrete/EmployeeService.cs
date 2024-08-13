using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ServiceResult<int>> AddEmployeeAsync(Employee employee)
        {
            try
            {
                var result = await _employeeRepository.AddEmployeeAsync(employee);
                return ServiceResult<int>.SuccessResult(result, "Çalışan başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Çalışan eklenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<int>> UpdateEmployeeAsync(Employee employee)
        {
            try
            {
                var result = await _employeeRepository.UpdateEmployeeAsync(employee);
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Çalışan başarıyla güncellendi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Çalışan güncellenemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Çalışan güncellenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<int>> DeleteEmployeeAsync(int employeeId, int deletedBy)
        {
            try
            {
                var result = await _employeeRepository.DeleteEmployeeAsync(employeeId, deletedBy);
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Çalışan başarıyla silindi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Çalışan silinemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Çalışan silinirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<Employee?>> GetEmployeeByIdAsync(int employeeId)
        {
            try
            {
                var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
                return employee != null
                    ? ServiceResult<Employee?>.SuccessResult(employee, "Çalışan başarıyla getirildi.", HttpStatusCode.OK)
                    : ServiceResult<Employee?>.FailureResult("Çalışan bulunamadı.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return ServiceResult<Employee?>.FailureResult($"Çalışan getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<IEnumerable<Employee>>> GetAllEmployeesAsync()
        {
            try
            {
                var employees = await _employeeRepository.GetAllEmployeesAsync();
                return ServiceResult<IEnumerable<Employee>>.SuccessResult(employees, "Tüm çalışanlar başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Employee>>.FailureResult($"Çalışanlar getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
