using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Net;
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
                return ServiceResult<int>.SuccessResult(result, "Shipper başarıyla eklendi.", HttpStatusCode.Created); // Başarı durumunda sonuç ve HTTP 201 durumu döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Shipper eklenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda sonuç ve HTTP 406 durumu döndürür
            }
        }

        // Var olan bir shipper'ı günceller
        public async Task<ServiceResult<int>> UpdateShipperAsync(Shipper shipper)
        {
            try
            {
                var result = await _shipperRepository.UpdateShipperAsync(shipper); // Repository'den shipper güncelleme işlemi
                return ServiceResult<int>.SuccessResult(result, "Shipper başarıyla güncellendi.", HttpStatusCode.OK); // Başarı durumunda sonuç ve HTTP 200 durumu döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Shipper güncellenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda sonuç ve HTTP 406 durumu döndürür
            }
        }

        // Bir shipper'ı siler
        public async Task<ServiceResult<int>> DeleteShipperAsync(int shipperId)
        {
            try
            {
                var result = await _shipperRepository.DeleteShipperAsync(shipperId); // Repository'den shipper silme işlemi
                return ServiceResult<int>.SuccessResult(result, "Shipper başarıyla silindi.", HttpStatusCode.OK); // Başarı durumunda sonuç ve HTTP 200 durumu döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Shipper silinirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda sonuç ve HTTP 406 durumu döndürür
            }
        }

        // ID ile shipper'ı getirir
        public async Task<ServiceResult<Shipper>> GetShipperByIdAsync(int shipperId)
        {
            try
            {
                var shipper = await _shipperRepository.GetShipperByIdAsync(shipperId); // Repository'den shipper getirme işlemi
                return shipper != null
                    ? ServiceResult<Shipper>.SuccessResult(shipper, "Shipper başarıyla getirildi.", HttpStatusCode.OK) // Başarı durumunda shipper'ı ve HTTP 200 durumunu döndürür
                    : ServiceResult<Shipper>.FailureResult("Shipper bulunamadı.", HttpStatusCode.NotFound); // Shipper bulunamadıysa hata ve HTTP 404 durumu döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<Shipper>.FailureResult($"Shipper getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda sonuç ve HTTP 406 durumu döndürür
            }
        }

        // Tüm shipper'ları getirir
        public async Task<ServiceResult<IEnumerable<Shipper>>> GetAllShippersAsync()
        {
            try
            {
                var shippers = await _shipperRepository.GetAllShippersAsync(); // Repository'den tüm shipper'ları getirme işlemi
                return ServiceResult<IEnumerable<Shipper>>.SuccessResult(shippers, "Tüm shipper'lar başarıyla getirildi.", HttpStatusCode.OK); // Başarı durumunda shipper'ları ve HTTP 200 durumunu döndürür
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Shipper>>.FailureResult($"Shipper'lar getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable); // Hata durumunda sonuç ve HTTP 406 durumu döndürür
            }
        }
    }
}
