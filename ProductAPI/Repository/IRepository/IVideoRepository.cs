using ProductAPI.Models;

namespace ProductAPI.Repository.IRepository
{
	public interface IVideoRepository:IRepository<Video>
	{
		Task<Video> GetVideoByUrlAsync(string url, bool tracked = true);
		Task<Video> UpdateAsync(Video entity);
		Task<IEnumerable<Video>> GetAllVideosForProductAsync(int productId, bool tracked = true);
	}
}
