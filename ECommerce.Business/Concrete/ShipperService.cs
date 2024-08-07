using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ECommerce.Business.Concrete
{
    public class ShipperService : IShipperService
    {
        private readonly IShipperRepository _shipperRepository; // IShipperRepository türünde bir veri erişim repository nesnesi

        public ShipperService(IShipperRepository shipperRepository)
        {
            _shipperRepository = shipperRepository; // Constructor'da dependency injection yoluyla repository nesnesini alır
        }

        // Yeni bir shipper ekler
        public async Task<ServiceResult<int>> AddShipperAsync(Shipper shipper)
        {
            try
            {
                var result = await _shipperRepository.AddShipperAsync(shipper); // Repository'den yeni shipper ekleme işlemi
                return ServiceResult<int>.SuccessResult(result, "Shipper başarıyla eklendi."); // Başarı durumunda sonuç döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Shipper eklenirken hata oluştu: {ex.Message}"); // Hata durumunda sonuç döndürür
            }
        }

        // Var olan bir shipper'ı günceller
        public async Task<ServiceResult<int>> UpdateShipperAsync(Shipper shipper)
        {
            try
            {
                var result = await _shipperRepository.UpdateShipperAsync(shipper); // Repository'den shipper güncelleme işlemi
                return ServiceResult<int>.SuccessResult(result, "Shipper başarıyla güncellendi."); // Başarı durumunda sonuç döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Shipper güncellenirken hata oluştu: {ex.Message}"); // Hata durumunda sonuç döndürür
            }
        }

        // Bir shipper'ı siler
        public async Task<ServiceResult<int>> DeleteShipperAsync(int shipperId)
        {
            try
            {
                var result = await _shipperRepository.DeleteShipperAsync(shipperId); // Repository'den shipper silme işlemi
                return ServiceResult<int>.SuccessResult(result, "Shipper başarıyla silindi."); // Başarı durumunda sonuç döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Shipper silinirken hata oluştu: {ex.Message}"); // Hata durumunda sonuç döndürür
            }
        }

        // ID ile shipper'ı getirir
        public async Task<ServiceResult<Shipper>> GetShipperByIdAsync(int shipperId)
        {
            try
            {
                var shipper = await _shipperRepository.GetShipperByIdAsync(shipperId); // Repository'den shipper getirme işlemi
                return shipper != null
                    ? ServiceResult<Shipper>.SuccessResult(shipper, "Shipper başarıyla getirildi.") // Başarı durumunda shipper'ı döndürür
                    : ServiceResult<Shipper>.FailureResult("Shipper bulunamadı."); // Şirket bulunamadıysa hata döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<Shipper>.FailureResult($"Shipper getirilirken hata oluştu: {ex.Message}"); // Hata durumunda sonuç döndürür
            }
        }

        // Tüm shipper'ları getirir
        public async Task<ServiceResult<IEnumerable<Shipper>>> GetAllShippersAsync()
        {
            try
            {
                var shippers = await _shipperRepository.GetAllShippersAsync(); // Repository'den tüm shipper'ları getirme işlemi
                return ServiceResult<IEnumerable<Shipper>>.SuccessResult(shippers, "Tüm shipper'lar başarıyla getirildi."); // Başarı durumunda shipper'ları döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Shipper>>.FailureResult($"Shipper'lar getirilirken hata oluştu: {ex.Message}"); // Hata durumunda sonuç döndürür
            }
        }
    }
}