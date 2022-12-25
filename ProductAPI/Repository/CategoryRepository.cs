using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Models;
using ProductAPI.Repository.IRepository;

namespace ProductAPI.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private readonly ApplicationDbContext _db;
		public CategoryRepository(ApplicationDbContext db) : base(db)
		{
			_db = db;
		}

		public async Task<IEnumerable<Category>> GetCategoriesForProductAsync(int productId, bool tracked = true)
		{
			// Retrieve the product from the database
			var product = await _db.Products.FirstOrDefaultAsync(p => p.ProductId == productId);

			// Return the product's categories
			return product.Categories;
		}

		public async Task<Category> GetCategoryByNameAsync(string categoryName, bool tracked = true)
		{
			if (tracked)
			{
				return await _db.Categories
					.FirstOrDefaultAsync(c => c.Name == categoryName);
			}
			else
			{
				return await _db.Categories
					.AsNoTracking()
					.FirstOrDefaultAsync(c => c.Name == categoryName);
			}
		}

		public async Task<IEnumerable<Category>> GetChildCategoriesAsync(int parentCategoryId, bool tracked = true)
		{
			var query = _db.Categories.Where(c => c.ParentCategoryId == parentCategoryId);
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.ToListAsync();
		}

		public async Task<IEnumerable<Category>> GetRootCategoriesAsync(bool tracked = true)
		{
			var query = _db.Categories.Where(c => c.ParentCategoryId == null);
			if (tracked)
			{
				query = query.AsTracking();
			}
			return await query.ToListAsync();
		}

		public async Task<Category> UpdateAsync(Category entity)
		{
			entity.UpdatedAt = DateTime.Now;
			_db.Categories.Update(entity);
			await _db.SaveChangesAsync();
			return entity;
		}
	}
}
