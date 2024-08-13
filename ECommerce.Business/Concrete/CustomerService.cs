using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System.Net;

namespace ECommerce.Business.Concrete
{
    // ICustomerService arayüzünü uygulayan CustomerService sınıfı
    public class CustomerService : ICustomerService
    {
        // Veri erişim katmanında müşteri verileriyle işlem yapacak repository
        private readonly ICustomerRepository _customerRepository;

        // CustomerService sınıfının yapıcı metodu, bağımlılık enjeksiyonunu sağlar
        public CustomerService(ICustomerRepository customerRepository)
        {
            // Verilen repository örneğini sınıf değişkenine atar
            _customerRepository = customerRepository;
        }

        // Tüm müşterileri asenkron olarak getirir
        public async Task<ServiceResult<IEnumerable<Customer>>> GetAllCustomersAsync()
        {
            try
            {
                // Repository'den tüm müşteri verilerini alır
                var customers = await _customerRepository.GetAllCustomersAsync();

                // Başarı durumunda müşterilerle birlikte başarılı bir ServiceResult döner
                // HTTP 200 (OK) durumu ile müşteriler döner
                return ServiceResult<IEnumerable<Customer>>.SuccessResult(customers, "Müşteriler başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
               
                return ServiceResult<IEnumerable<Customer>>.FailureResult($"Müşteriler getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Verilen müşteri ID'sine göre müşteriyi asenkron olarak getirir
        public async Task<ServiceResult<Customer>> GetCustomerByIdAsync(int customerId)
        {
            try
            {
                // Repository'den müşteri verisini alır
                var customer = await _customerRepository.GetCustomerByIdAsync(customerId);

                // Müşteri bulunursa başarılı bir ServiceResult döner, bulunamazsa başarısız döner
                // Müşteri bulunursa HTTP 200 (OK), bulunamazsa HTTP 404 (Not Found) döner
                return customer != null
                    ? ServiceResult<Customer>.SuccessResult(customer, "Müşteri başarıyla getirildi.", HttpStatusCode.OK)
                    : ServiceResult<Customer>.FailureResult("Müşteri bulunamadı.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                
                return ServiceResult<Customer>.FailureResult($"Müşteri getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Yeni bir müşteri ekler ve eklenen müşterinin ID'sini döner
        public async Task<ServiceResult<int>> AddCustomerAsync(Customer customer)
        {
            try
            {
                // Repository'ye yeni müşteriyi ekler ve müşteri ID'sini alır
                var customerId = await _customerRepository.AddCustomerAsync(customer);

                // Başarı durumunda yeni müşteri ID'si ile başarılı bir ServiceResult döner
                // HTTP 201 (Created) durumu ile yeni müşteri ID'si döner
                return ServiceResult<int>.SuccessResult(customerId, "Müşteri başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                
                return ServiceResult<int>.FailureResult($"Müşteri eklenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Var olan müşteriyi günceller
        public async Task<ServiceResult<int>> UpdateCustomerAsync(int customerId, Customer customer)
        {
            try
            {
                // Gelen müşteri ID'sinin, güncellenmek istenen müşteri ID'siyle uyuşup uyuşmadığını kontrol eder
                if (customerId != customer.CustomerId)
                {
                    // ID uyuşmazsa başarısız bir ServiceResult döner
                    // HTTP 400 (Bad Request) durumu ile ID uyuşmazlığı mesajı döner
                    return ServiceResult<int>.FailureResult("Müşteri ID'si uyuşmuyor.", HttpStatusCode.Conflict);
                }

                // Repository'yi kullanarak müşteri verisini günceller ve etkilenen satır sayısını alır
                var rowsAffected = await _customerRepository.UpdateCustomerAsync(customer);

                // Güncelleme başarılıysa etkilenen satır sayısı ile başarılı bir ServiceResult döner
                // Güncelleme başarılıysa HTTP 200 (OK), başarısızsa HTTP 404 (Not Found) döner
                return rowsAffected > 0
                    ? ServiceResult<int>.SuccessResult(rowsAffected, "Müşteri başarıyla güncellendi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Müşteri güncellenemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
              
                return ServiceResult<int>.FailureResult($"Müşteri güncellenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Var olan müşteri kaydını siler
        public async Task<ServiceResult<int>> DeleteCustomerAsync(int customerId, int deletedBy)
        {
            try
            {
                // Repository'yi kullanarak müşteri kaydını siler ve etkilenen satır sayısını alır
                var rowsAffected = await _customerRepository.DeleteCustomerAsync(customerId, deletedBy);

                // Silme işlemi başarılıysa etkilenen satır sayısı ile başarılı bir ServiceResult döner
                // Silme başarılıysa HTTP 200 (OK), başarısızsa HTTP 404 (Not Found) döner
                return rowsAffected > 0
                    ? ServiceResult<int>.SuccessResult(rowsAffected, "Müşteri başarıyla silindi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Müşteri silinemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
            
                return ServiceResult<int>.FailureResult($"Müşteri silinirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
