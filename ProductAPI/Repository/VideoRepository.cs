using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;

namespace ProductAPI.Repository
{
	public class VideoRepository:Repository<Video>, IVideoRepository
	{
		private readonly ApplicationDbContext _db;
		public VideoRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<IEnumerable<Video>> GetAllVideosForProductAsync(int productId, bool tracked = true)
		{
			var query = _db.Videos.Where(v => v.ProductId == productId);
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.ToListAsync();
		}

		public async Task<Video> GetVideoByUrlAsync(string url, bool tracked = true)
		{
			var query = _db.Videos.Where(i => i.Url == url);
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<Video> UpdateAsync(Video entity)
		{
			entity.UpdatedAt = DateTime.Now;
			_db.Videos.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
