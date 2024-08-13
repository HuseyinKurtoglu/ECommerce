using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.Business.Absract
{
    public interface IImageService
    {
        Task<ServiceResult<int>> AddImageAsync(Image image);
        Task<ServiceResult<int>> UpdateImageAsync(Image image);
        Task<ServiceResult<int>> DeleteImageAsync(int imageId, int deletedBy);
        Task<ServiceResult<Image?>> GetImageByIdAsync(int imageId);
        Task<ServiceResult<IEnumerable<Image>>> GetAllImagesAsync();
    }
}
