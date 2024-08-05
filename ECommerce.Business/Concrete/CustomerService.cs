using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
        {
            try
            {
                return await _customerRepository.GetAllCustomersAsync();
            }
            catch (Exception ex)
            {
               
                throw new Exception("Error retrieving customers", ex);
            }
        }

        public async Task<Customer> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                return await _customerRepository.GetCustomerByIdAsync(customerId);
            }
            catch (Exception ex)
            {
                
                throw new Exception($"Error retrieving customer with ID {customerId}", ex);
            }
        }

        public async Task<int> AddCustomerAsync(Customer customer)
        {
            try
            {
                return await _customerRepository.AddCustomerAsync(customer);
            }
            catch (Exception ex)
            {
                
                throw new Exception("Error adding customer", ex);
            }
        }

        public async Task<int> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                return await _customerRepository.UpdateCustomerAsync(customer);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Error updating customer with ID {customer.CustomerId}", ex);
            }
        }

        public async Task<int> DeleteCustomerAsync(int customerId, int deletedBy)
        {
            try
            {
                return await _customerRepository.DeleteCustomerAsync(customerId, deletedBy);
            }
            catch (Exception ex)
            {
               
                throw new Exception($"Error deleting customer with ID {customerId}", ex);
            }
        }
    }

}
