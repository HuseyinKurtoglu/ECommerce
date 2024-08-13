using ECommerce.DataAcces.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ECommerce.DataAcces.Absract
{
    public interface IImageRepository
    {
        Task<int> AddImageAsync(Image image);
        Task<int> UpdateImageAsync(Image image);
        Task<int> DeleteImageAsync(int imageId, int deletedBy);
        Task<Image?> GetImageByIdAsync(int imageId);
        Task<IEnumerable<Image>> GetAllImagesAsync();
    }
}
