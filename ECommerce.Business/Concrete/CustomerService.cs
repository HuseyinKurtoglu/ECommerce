// ECommerce iş mantığı katmanında müşteri servislerini tanımlar.
using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;

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
                return ServiceResult<IEnumerable<Customer>>.SuccessResult(customers, "Müşteriler başarıyla getirildi.");
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                return ServiceResult<IEnumerable<Customer>>.FailureResult($"Müşteriler getirilirken hata oluştu: {ex.Message}");
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
                return customer != null
                    ? ServiceResult<Customer>.SuccessResult(customer, "Müşteri başarıyla getirildi.")
                    : ServiceResult<Customer>.FailureResult("Müşteri bulunamadı.");
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                return ServiceResult<Customer>.FailureResult($"Müşteri getirilirken hata oluştu: {ex.Message}");
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
                return ServiceResult<int>.SuccessResult(customerId, "Müşteri başarıyla eklendi.");
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                return ServiceResult<int>.FailureResult($"Müşteri eklenirken hata oluştu: {ex.Message}");
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
                    return ServiceResult<int>.FailureResult("Müşteri ID'si uyuşmuyor.");
                }

                // Repository'yi kullanarak müşteri verisini günceller ve etkilenen satır sayısını alır
                var rowsAffected = await _customerRepository.UpdateCustomerAsync(customer);
                // Güncelleme başarılıysa etkilenen satır sayısı ile başarılı bir ServiceResult döner
                return rowsAffected > 0
                    ? ServiceResult<int>.SuccessResult(rowsAffected, "Müşteri başarıyla güncellendi.")
                    : ServiceResult<int>.FailureResult("Müşteri güncellenemedi.");
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                return ServiceResult<int>.FailureResult($"Müşteri güncellenirken hata oluştu: {ex.Message}");
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
                return rowsAffected > 0
                    ? ServiceResult<int>.SuccessResult(rowsAffected, "Müşteri başarıyla silindi.")
                    : ServiceResult<int>.FailureResult("Müşteri silinemedi.");
            }
            catch (Exception ex)
            {
                // Hata durumunda hata mesajıyla birlikte başarısız bir ServiceResult döner
                return ServiceResult<int>.FailureResult($"Müşteri silinirken hata oluştu: {ex.Message}");
            }
        }
    }
}
