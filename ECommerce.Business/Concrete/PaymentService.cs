using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq; // LINQ kullanımı için gerekli
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository; // IPaymentRepository türünden bir bağımlılık
        private readonly IValidator<Payment> _paymentValidator; // Payment modelini doğrulamak için FluentValidation bağımlılığı

        // Constructor: Bağımlılıkları enjekte eder
        public PaymentService(IPaymentRepository paymentRepository, IValidator<Payment> paymentValidator)
        {
            _paymentRepository = paymentRepository;
            _paymentValidator = paymentValidator;
        }

        // Tüm ödemeleri getirir
        public async Task<ServiceResult<IEnumerable<Payment>>> GetAllPaymentsAsync()
        {
            try
            {
                // Ödemeleri almak için repository yöntemini çağırır
                var result = await _paymentRepository.GetAllPaymentsAsync();
                // Başarı durumunda sonuç döner
                return ServiceResult<IEnumerable<Payment>>.SuccessResult(result, "Ödemeler başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // Hata durumunda sonuç döner
                return ServiceResult<IEnumerable<Payment>>.FailureResult($"Ödemeler getirilirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir ödeme bilgilerini getirir
        public async Task<ServiceResult<Payment>> GetPaymentByIdAsync(int paymentId)
        {
            try
            {
                // Ödeme bilgilerini almak için repository yöntemini çağırır
                var payment = await _paymentRepository.GetPaymentByIdAsync(paymentId);
                // Ödeme bulunduysa başarı döner, aksi takdirde hata döner
                return payment != null
                    ? ServiceResult<Payment>.SuccessResult(payment, "Ödeme başarıyla getirildi.", HttpStatusCode.OK)
                    : ServiceResult<Payment>.FailureResult("Ödeme bulunamadı.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda sonuç döner
                return ServiceResult<Payment>.FailureResult($"Ödeme getirilirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Yeni bir ödeme ekler
        public async Task<ServiceResult<int>> AddPaymentAsync(Payment payment)
        {
            try
            {
                // Ödeme verilerini doğrular
                var validationResult = await _paymentValidator.ValidateAsync(payment);
                if (!validationResult.IsValid)
                {
                    // Doğrulama başarısızsa hata mesajlarını toplar
                    var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return ServiceResult<int>.FailureResult(errorMessage, HttpStatusCode.BadRequest);
                }

                // Yeni ödemeyi eklemek için repository yöntemini çağırır
                var result = await _paymentRepository.AddPaymentAsync(payment);
                // Başarı durumunda sonuç döner
                return ServiceResult<int>.SuccessResult(result, "Ödeme başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                // Hata durumunda sonuç döner
                return ServiceResult<int>.FailureResult($"Ödeme eklenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Var olan bir ödemeyi günceller
        public async Task<ServiceResult<int>> UpdatePaymentAsync(int paymentId, Payment payment)
        {
            try
            {
                // Ödeme verilerini doğrular
                var validationResult = await _paymentValidator.ValidateAsync(payment);
                if (!validationResult.IsValid)
                {
                    // Doğrulama başarısızsa hata mesajlarını toplar
                    var errorMessage = string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage));
                    return ServiceResult<int>.FailureResult(errorMessage, HttpStatusCode.BadRequest);
                }

                // Ödemeyi güncellemek için repository yöntemini çağırır
                var result = await _paymentRepository.UpdatePaymentAsync(paymentId, payment);
                // Güncelleme başarılıysa sonuç döner, aksi takdirde hata döner
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Ödeme başarıyla güncellendi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Ödeme güncellenemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda sonuç döner
                return ServiceResult<int>.FailureResult($"Ödeme güncellenirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        // Belirli bir ödemeyi siler
        public async Task<ServiceResult<int>> DeletePaymentAsync(int paymentId, int deletedBy)
        {
            try
            {
                // Ödemeyi silmek için repository yöntemini çağırır
                var result = await _paymentRepository.DeletePaymentAsync(paymentId, deletedBy);
                // Silme başarılıysa sonuç döner, aksi takdirde hata döner
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Ödeme başarıyla silindi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Ödeme silinemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                // Hata durumunda sonuç döner
                return ServiceResult<int>.FailureResult($"Ödeme silinirken bir hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
