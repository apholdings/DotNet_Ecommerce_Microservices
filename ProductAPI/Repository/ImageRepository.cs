using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;

namespace ProductAPI.Repository
{
	public class ImageRepository : Repository<Image>, IImageRepository
	{
		private readonly ApplicationDbContext _db;
		public ImageRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<IEnumerable<Image>> GetAllImagesForProductAsync(int productId, bool tracked = true)
		{
			return await _db.Images.Where(i => i.ProductId == productId).ToListAsync();
		}

		public async Task<Image> GetImageByUrlAsync(string url, bool tracked = true)
		{
			var query = _db.Images.Where(i => i.Url == url);
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.FirstOrDefaultAsync();
		}

		public async Task<Image> UpdateAsync(Image entity)
		{
			entity.UpdatedAt = DateTime.Now;
			_db.Images.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
