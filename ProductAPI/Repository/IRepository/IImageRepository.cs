using ProductAPI.Models;

namespace ProductAPI.Repository.IRepository
{
	public interface IImageRepository:IRepository<Image>
	{
		Task<Image> GetImageByUrlAsync(string url, bool tracked = true);
		Task<Image> UpdateAsync(Image entity);
		Task<IEnumerable<Image>> GetAllImagesForProductAsync(int productId, bool tracked = true);
	}
}
