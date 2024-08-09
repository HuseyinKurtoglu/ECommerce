using ECommerce.Business.Absract;
using ECommerce.DataAcces.Absract;
using ECommerce.DataAcces.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace ECommerce.Business.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;

        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }

        public async Task<ServiceResult<int>> AddImageAsync(Image image)
        {
            try
            {
                var result = await _imageRepository.AddImageAsync(image);
                return ServiceResult<int>.SuccessResult(result, "Resim başarıyla eklendi.", HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Resim eklenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<int>> UpdateImageAsync(Image image)
        {
            try
            {
                var result = await _imageRepository.UpdateImageAsync(image);
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Resim başarıyla güncellendi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Resim güncellenemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Resim güncellenirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<int>> DeleteImageAsync(int imageId, int deletedBy)
        {
            try
            {
                var result = await _imageRepository.DeleteImageAsync(imageId, deletedBy);
                return result > 0
                    ? ServiceResult<int>.SuccessResult(result, "Resim başarıyla silindi.", HttpStatusCode.OK)
                    : ServiceResult<int>.FailureResult("Resim silinemedi.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return ServiceResult<int>.FailureResult($"Resim silinirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<Image?>> GetImageByIdAsync(int imageId)
        {
            try
            {
                var image = await _imageRepository.GetImageByIdAsync(imageId);
                return image != null
                    ? ServiceResult<Image?>.SuccessResult(image, "Resim başarıyla getirildi.", HttpStatusCode.OK)
                    : ServiceResult<Image?>.FailureResult("Resim bulunamadı.", HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                return ServiceResult<Image?>.FailureResult($"Resim getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }

        public async Task<ServiceResult<IEnumerable<Image>>> GetAllImagesAsync()
        {
            try
            {
                var images = await _imageRepository.GetAllImagesAsync();
                return ServiceResult<IEnumerable<Image>>.SuccessResult(images, "Tüm resimler başarıyla getirildi.", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return ServiceResult<IEnumerable<Image>>.FailureResult($"Resimler getirilirken hata oluştu: {ex.Message}", HttpStatusCode.NotAcceptable);
            }
        }
    }
}
